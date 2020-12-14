using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.Movement;
using IND.Core.Shared.Statics;

namespace IND.Core.AISystems.States
{[System.Serializable]
    public class AIState_Search
    {
        private AIStateController stateController;
        private AIMovementController movementController;

        private Vector3 originalStartSearchAreaPosition;
        private Vector3 currentSearchAreaPosition;
        private Vector3 startingPositionInWorld;
        private bool reachedStartSearchPosition = false;
        protected bool isObservingCurrentSearchPosition = false;

        public float searchDistance = 5f;
        public float subSearchTargetDuration = 1f;
        public float totalSearchDuration = 10f;

        private float minDistanceToStop = 0.2f;
        private float curDistanceFromTarget;

        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
            movementController = stateCon.GetComponent<AIMovementController>();

            startingPositionInWorld = stateCon.transform.position;
        }

        public void Tick()
        {
            if(CheckIfHasReachedSearchPosition() == true)
            {
                if (reachedStartSearchPosition == false)
                {
                    OnSearchAreaStartDestinationReached();
                }
            }
        }

        public void AssignSearchTask(Vector3 positionToSearch)
        {
            if (stateController.currentState == AIStateType.COMBAT)
                return;

            stateController.ChangeState(AIStateType.SEARCH);

            originalStartSearchAreaPosition = positionToSearch;
            currentSearchAreaPosition = originalStartSearchAreaPosition;

            movementController.SetAgentDestination(positionToSearch);
        }

        bool CheckIfHasReachedSearchPosition()
        {
            curDistanceFromTarget = Vector3.Distance(stateController.transform.position, currentSearchAreaPosition);
            if(curDistanceFromTarget > minDistanceToStop)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void EndSearch()
        {
            reachedStartSearchPosition = false;
            stateController.StopCoroutine("ObserveSubAreaTargetTimer");
            movementController.StopAgent();
            stateController.ChangeState(stateController.startState);
        }
        private void OnObservationEnd() //Called when the current sub area timer has been oberved
        {
            GetNewObservationSubTarget();
            movementController.StopAgent();
        }

        //Called when the Start Destination for searching is reached
        public void OnSearchAreaStartDestinationReached()
        {
            reachedStartSearchPosition = true;
            isObservingCurrentSearchPosition = true;
            stateController.StartCoroutine("SearchAreaTimer");
            stateController.StartCoroutine("ObserveSubAreaTargetTimer");
        }

        void GetNewObservationSubTarget()
        {
            Vector3 randomDirection = originalStartSearchAreaPosition + Random.insideUnitSphere * searchDistance;
            randomDirection.y = stateController.transform.position.y;
            currentSearchAreaPosition = GetNearestNavMeshPoint.GetClosestNavMeshPoint(randomDirection);
            isObservingCurrentSearchPosition = false;
        }
        private IEnumerator ObserveSubAreaTargetTimer()
        {
            yield return new WaitForSeconds(subSearchTargetDuration);
            OnObservationEnd();
        }

        private IEnumerator SearchAreaTimer()
        {
            yield return new WaitForSeconds(totalSearchDuration);
            EndSearch();
        }

        public void OnStateEnter()
        {

        }

        public void OnStateExit()
        {

        }
    }
}