using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Combat;
using IND.Core.Cameras;
using IND.Core.AISystems;

namespace IND.Core.Player.Movement
{
    public class PlayerMovementController : IND_Mono
    {
        [InlineEditor] public PlayerMovementData movementData;
        private Rigidbody rigidBod;
        private Camera gameCamera;
        private Transform aimTarget;

        private float movementSpeed;
        private Vector3 targetPoint;
        private Quaternion targetRotation;

        private Vector3 moveVelocity;
        Vector3 camForward;
        Vector3 move;
        Vector3 moveInput;

        private int curNotificationTickRate = 0;

        private ScriptableFloat deltaTime;
        private ScriptableFloat verticalNormalizedMoveAmount;
        private ScriptableFloat horizontallNormalizedMoveAmount;

        private PlayerMovementInputs movementInput;

        private ScriptableBool isPlayerInAction;

        #region Fixed Rotation Value Positions
        private Vector3 topForwardMovement = new Vector3(0, 0, 0);
        private Vector3 topRightMovement = new Vector3(0, 45, 0);
        private Vector3 rightMovement = new Vector3(0, 90, 0);
        private Vector3 bottomRightMovement = new Vector3(0, 135, 0);
        private Vector3 bottomMovement = new Vector3(0, 180, 0);
        private Vector3 bottomLeftMovement = new Vector3(0, 225, 0);
        private Vector3 leftMovement = new Vector3(0, 270, 0);
        private Vector3 topLeftMovement = new Vector3(0, 315, 0);
        #endregion
        public override void Init()
        {
            movementSpeed = movementData.moveSpeed;
            rigidBod = GetComponent<Rigidbody>();
            gameCamera = FindObjectOfType<CamControllerGameplay>().GetComponent<Camera>();
            aimTarget = FindObjectOfType<PlayerAimTargetIdentifier>().transform;

            verticalNormalizedMoveAmount = Resources.Load("Player/Movement/Player - Normalized Movement Vertical") as ScriptableFloat;
            horizontallNormalizedMoveAmount = Resources.Load("Player/Movement/Player - Normalized Movement Horizontal") as ScriptableFloat;

            isPlayerInAction = Resources.Load("Player/Actions/Is Player In Action") as ScriptableBool;
            movementInput = Resources.Load("Player/Movement/Movement Inputs") as PlayerMovementInputs;

            deltaTime = Resources.Load("Core Variables/DeltaTimer") as ScriptableFloat;
        }

        public override void Tick()
        {
            if (isPlayerInAction.value == true)
            {
                return;
            }

            CheckMovementKeys();

            LookAtTarget();

            camForward = Vector3.Scale(gameCamera.transform.up, new Vector3(1, 0, 1)).normalized;
            move = movementInput.verticalInput.value * camForward + movementInput.horizontalInput.value * gameCamera.transform.right;

            if (move.magnitude > 1)
            {
                move.Normalize();
            }

            MovePlayer(move);


            moveInput = new Vector3(movementInput.horizontalInput.value, 0f, movementInput.verticalInput.value);
            moveVelocity = moveInput * movementSpeed;
            rigidBod.velocity = moveVelocity;

            if (movementInput.isPressingMovementKeys == true)
            {
                curNotificationTickRate++;
                if (curNotificationTickRate == movementData.notificationTickRate) ;
                {
                    curNotificationTickRate = 0;
                    AIManager.singleton.SendNotificationToAi(movementData.notifcationDistance, transform.position);
                }
            }
        }

        public override void FixedTick()
        {

        }

        private void CheckMovementKeys()
        {
            if (movementInput.horizontalInput.value > 0.2)
            {
                movementInput.rightMovementInput.value = true;
            }
            else
            {
                movementInput.rightMovementInput.value = false;
            }

            if (movementInput.horizontalInput.value < -0.2)
            {
                movementInput.leftMovementInput.value = true;
            }
            else
            {
                movementInput.leftMovementInput.value = false;
            }

            if (movementInput.verticalInput.value > 0.2)
            {
                movementInput.forwardMovementInput.value = true;
            }
            else
            {
                movementInput.forwardMovementInput.value = false;
            }

            if (movementInput.verticalInput.value < -0.2)
            {
                movementInput.backwardsMovementInput.value = true;
            }
            else
            {
                movementInput.backwardsMovementInput.value = false;
            }

            if (movementInput.rightMovementInput.value || movementInput.leftMovementInput.value || movementInput.forwardMovementInput.value || movementInput.backwardsMovementInput.value)
            {
                if (movementInput.isPressingMovementKeys.value == false)
                {
                    movementInput.isPressingMovementKeys.value = true;
                }
            }
            else
            {
                if (movementInput.isPressingMovementKeys.value == true)
                {
                    movementInput.isPressingMovementKeys.value = false;
                }
            }
        }

        void MovePlayer(Vector3 move)
        {
            if (move.magnitude > 1)
            {
                move.Normalize();
            }

            moveInput = move;

            ConvertMoveInput();
        }

        void ConvertMoveInput()
        {
            Vector3 localMove = transform.InverseTransformDirection(moveInput);

            horizontallNormalizedMoveAmount.value = localMove.x;
            verticalNormalizedMoveAmount.value = localMove.z;
        }

        void LookAtTarget()
        {
            targetPoint = new Vector3(aimTarget.position.x, transform.position.y, aimTarget.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, deltaTime.value * movementData.movementRotationSpeed.value);
        }
    }
}