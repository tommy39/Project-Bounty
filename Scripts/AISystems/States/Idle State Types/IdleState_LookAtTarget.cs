using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.States;
using IND.Core.Characters.Animations;
using IND.Core.AISystems.LookAtTargetsNetwork;

namespace IND.Core.AISystems.States.Idle
{
    [System.Serializable]
    public class IdleState_LookAtTarget
    {
        private AIStateController stateController;
        private ScriptableFloat deltaTime;

        public LookAtTargetsNetworkController targetNetwork;

        public int currentTargetNumber = 0;
        public float rotateSpeed = 2f;

        private Transform currenTargetToLookAt;
        private float lookAtTargetDuration;
        private Vector3 targetPoint;
        private Quaternion targetRotation;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
            deltaTime = GetResource.DeltaTimer();

            if (currentTargetNumber >= targetNetwork.targets.Count - 1)
            {
                currentTargetNumber = 0;
            }

            currenTargetToLookAt = targetNetwork.targets[currentTargetNumber].transform;
            lookAtTargetDuration = targetNetwork.targets[currentTargetNumber].lookDuration;

            stateController.StartCoroutine(LookAtTargetTimer());
        }

        public void Tick()
        {
            targetPoint = new Vector3(currenTargetToLookAt.position.x, stateController.transform.position.y, currenTargetToLookAt.position.z) - stateController.transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            stateController.transform.rotation = Quaternion.Slerp(stateController.transform.rotation, targetRotation, deltaTime.value * rotateSpeed);
        }

        public void GetNextLookAtTarget()
        {
            if (targetNetwork.targets.Count == 0)
                return;

            if(currentTargetNumber == targetNetwork.targets.Count - 1)
            {
                currentTargetNumber = 0;
            }
            else
            {
                currentTargetNumber++;
            }

            currenTargetToLookAt = targetNetwork.targets[currentTargetNumber].transform;
            lookAtTargetDuration = targetNetwork.targets[currentTargetNumber].lookDuration;

            stateController.StartCoroutine(LookAtTargetTimer());
        }

        private IEnumerator LookAtTargetTimer()
        {
            yield return new WaitForSeconds(lookAtTargetDuration);
            GetNextLookAtTarget();
        }
         
    }
}