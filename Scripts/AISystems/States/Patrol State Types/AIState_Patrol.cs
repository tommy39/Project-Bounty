using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.Waypoints;
using IND.Core.Characters.Animations;
using IND.Core.AISystems.States.Patrol;
using IND.Core.AISystems.Movement;

namespace IND.Core.AISystems.States
{[System.Serializable]
    public class AIState_Patrol
    {
        private AIStateController stateController;
        private AIMovementController movementController;
        private AnimationController animationController;

        [InlineEditor] [Required] public WaypointNetwork waypointNetwork;
        [PropertyTooltip("Value is minused by 1, so use 1 for the first in list")]
        public int currentWaypointInList = 1;

        private Vector3 currentDestination;
        public float stoppingDistanceFromDestination = 0.8f;
        private float currentDistanceFromDestination;
        private WaypointController currentWaypoint;
        private bool isMovingToWaypoint = false;
        private int currentLookAtTarget = 0;
        private bool isWaitingAtWaypoint = false;
        private bool isWaitingAtLookAtTarget = false;

        public float rotateSpeed = 2f;
        private ScriptableFloat deltaTime;
        private Transform currenTargetToLookAt;
        private Vector3 targetPoint;
        private Quaternion targetRotation;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
            movementController = stateCon.GetComponent<AIMovementController>();
            animationController = stateCon.GetComponent<AnimationController>();
            deltaTime = GetResource.DeltaTimer();

            if (waypointNetwork == null)
                return;
            currentWaypointInList--;

            if(currentWaypointInList <= 0)
            {
                currentWaypointInList++;
            }  

            if(currentWaypointInList >= waypointNetwork.wayPoints.Count)
            {
                currentWaypointInList = 0;
            }
       
        }
        public void Tick()
        {
            if(isMovingToWaypoint == true)
            {
                if(isWaitingAtLookAtTarget)
                {
                    RotateToLookAtTarget();
                }

                if(IsInStoppingDistanceOfTargetDestination() == true)
                {                  
                    StopAtWaypointReached();
                }
                else
                {
                    return;
                }
            }
            else
            {
                GetNextWaypointInList();
            }          
        }

        bool IsInStoppingDistanceOfTargetDestination()
        {
            currentDistanceFromDestination = Vector3.Distance(stateController.transform.position, currentDestination);
            if(currentDistanceFromDestination < stoppingDistanceFromDestination)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetNextWaypointInList()
        {
            if(currentWaypointInList == waypointNetwork.wayPoints.Count - 1)
            {
                currentWaypointInList = 0;
            }
            else
            {
                currentWaypointInList++;
            }
            isWaitingAtWaypoint = false;
            SetWaypoint();
        }

        void SetWaypoint()
        {
            currentWaypoint = waypointNetwork.wayPoints[currentWaypointInList];
            currentDestination = new Vector3(currentWaypoint.transform.position.x, stateController.transform.position.y, currentWaypoint.transform.position.z);
            SetAnimatorToWalk();
            movementController.SetAgentDestination(currentDestination);
            isMovingToWaypoint = true;
        }

        void StopAtWaypointReached()
        {
            if (isWaitingAtWaypoint == true)
                return;

            switch (waypointNetwork.wayPoints[currentWaypointInList].onReachTaskType)
            {
                case WaypointOnReachTaskType.MOVE_TO_NEXT_WAYPOINT:
                    GetNextWaypointInList();
                    isWaitingAtWaypoint = false;
                    break;
                case WaypointOnReachTaskType.STOP_AND_WAIT:
                    movementController.StopAgent();
                    stateController.StartCoroutine(WaitAtWaypointTimer(waypointNetwork.wayPoints[currentWaypointInList].stopAndWaitTime));
                    isWaitingAtWaypoint = true;
                    break;
                case WaypointOnReachTaskType.STOP_LOOK_AT_TARGETS:
                    currentLookAtTarget = 0;
                    GetNextLookAtTarget();
                    isWaitingAtWaypoint = true;
                    break;
            }
        }

        public void SetAnimatorToWalk()
        {
            animationController.SetAnimFloat(animationController.animData.verticalFloat, 1f);
            animationController.SetAnimFloat(animationController.animData.horizontalFloat, 0);
        }

        public void SetAnimatorToStandingStill()
        {
            animationController.SetAnimFloat(animationController.animData.verticalFloat, 0);
            animationController.SetAnimFloat(animationController.animData.horizontalFloat, 0);
        }

        private IEnumerator WaitAtWaypointTimer(float timer)
        {
            yield return new WaitForSeconds(timer);
            GetNextWaypointInList();
        }

        private IEnumerator StopAndLookAtTargetTimer(float timer)
        {
            SetAnimatorToStandingStill();
            yield return new WaitForSeconds(timer);
            currentLookAtTarget++;
            GetNextLookAtTarget();
        }

        void GetNextLookAtTarget()
        {
            if (currentLookAtTarget == waypointNetwork.wayPoints[currentWaypointInList].stopAndLookTargets.Count)
            {
                GetNextWaypointInList();
                isWaitingAtLookAtTarget = false;
            }
            else
            {
                currenTargetToLookAt = waypointNetwork.wayPoints[currentWaypointInList].stopAndLookTargets[currentLookAtTarget].transform;
                isWaitingAtLookAtTarget = true;
                stateController.StartCoroutine(StopAndLookAtTargetTimer(waypointNetwork.wayPoints[currentWaypointInList].stopAndLookTargets[currentLookAtTarget].lookDuration));
            }
        }

        void RotateToLookAtTarget()
        {
            targetPoint = new Vector3(currenTargetToLookAt.position.x, stateController.transform.position.y, currenTargetToLookAt.position.z) - stateController.transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            stateController.transform.rotation = Quaternion.Slerp(stateController.transform.rotation, targetRotation, deltaTime.value * rotateSpeed);
        }
        public void OnStateEnter()
        {

        }

        public void OnStateExit()
        {

        }
    }
}