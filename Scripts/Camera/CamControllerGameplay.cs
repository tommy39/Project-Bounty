using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
using IND.Core.Player.Combat;
using IND.Core.Player;

namespace IND.Core.Cameras
{
    public class CamControllerGameplay : IND_Mono
    {
        private Transform playerFocusPoint;
        private Transform playerTransform;
        private Transform aimIKTarget;

        private CinemachineVirtualCamera cm_VirtualCamera;

        private float curDistance;
        [InlineEditor] public CameraCinemaMachineData cameraData;
        private Vector3 eulerRotation;

        private ScriptableInputBoolAction extendCameraInputAction;
        public override void Init()
        {
            playerFocusPoint = FindObjectOfType<PlayerController>().cameraFocusPoint;
            playerTransform = FindObjectOfType<PlayerController>().transform;
            cm_VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            aimIKTarget = FindObjectOfType<PlayerAimTargetIdentifier>().transform;

            extendCameraInputAction = Resources.Load("Input Actions/Input Action Extend Camera Boundary") as ScriptableInputBoolAction;

            if (cameraData.followPlayer)
            {
                cm_VirtualCamera.Follow = playerFocusPoint;
            }
        }

        public override void Tick()
        {
            RotateObjectTowardsMouse();
            MoveObjectForward();
        }

        void RotateObjectTowardsMouse()
        {
            eulerRotation = new Vector3(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
            playerFocusPoint.rotation = Quaternion.Euler(eulerRotation);
        }

        void MoveObjectForward()
        {
            curDistance = Vector3.Distance(new Vector3(playerTransform.position.x, playerFocusPoint.position.y, playerTransform.position.z), playerFocusPoint.position);

            if (extendCameraInputAction.value.value == false)
            { // Not Extended Aim

                if (curDistance > cameraData.deadZone1Distance)
                {
                    if (curDistance > cameraData.deadZone1Distance + 1f)
                    {
                        playerFocusPoint.position = Vector3.MoveTowards(playerFocusPoint.position, new Vector3(playerTransform.position.x, playerFocusPoint.position.y, playerTransform.position.z), (cameraData.focusPointFastReturnSpeed * Time.deltaTime));
                    }
                    else
                    {
                        playerFocusPoint.position = Vector3.MoveTowards(playerFocusPoint.position, new Vector3(playerTransform.position.x, playerFocusPoint.position.y, playerTransform.position.z), (cameraData.focusPointMoveSpeedRegular * Time.deltaTime));
                    }
                }
                else
                {
                    playerFocusPoint.position = Vector3.MoveTowards(playerFocusPoint.position, new Vector3(aimIKTarget.position.x, playerFocusPoint.position.y, aimIKTarget.position.z), (cameraData.focusPointMoveSpeedRegular * Time.deltaTime));
                }
            }
            else
            { // Is Extended Aim

                if (curDistance > cameraData.deadZone2Distance)
                {
                    playerFocusPoint.position = Vector3.MoveTowards(playerFocusPoint.position, new Vector3(playerTransform.position.x, playerFocusPoint.position.y, playerTransform.position.z), (cameraData.focusPointMoveSpeedDeadzone2 * Time.deltaTime));
                }
                else
                {
                    playerFocusPoint.position = Vector3.MoveTowards(playerFocusPoint.position, new Vector3(aimIKTarget.position.x, playerFocusPoint.position.y, aimIKTarget.position.z), (cameraData.focusPointMoveSpeedDeadzone2 * Time.deltaTime));
                }
            }
        }
    }
}