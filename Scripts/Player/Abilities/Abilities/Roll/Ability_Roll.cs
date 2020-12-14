using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Movement;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.Animations;

namespace IND.Core.Player.Abilities.Roll
{
    [CreateAssetMenu(fileName = "Ability_Roll", menuName = "IND/Core/Player/Abilities/Ability_Roll")]
    public class Ability_Roll : Ability
    {
        public float rollDuration;
        [PropertyRange(0, 20)] public float rollSpeed = 1f;
        public float rollDistance = 10f;
        public float obstacleDistanceCheck = 5f;

        private RollDirection currentRollDirection;
        private Vector3 targetRollPosition;
        private Vector3 rayDirection;
        private RaycastHit rayHit;

        [InlineEditor] [Required] public ScriptableLayerMask obstaclesLayerMask;
        private ScriptableFloat deltaTimer;
        private ScriptableBool isInAction;
        private PlayerMovementController movementController;
        private ScriptableBool isPressingMovementKeys;
        private HitboxController hitboxController;
        private PlayerAnimationController animController;
        private PlayerMovementInputs movementInput;

        public override void InitAbility(AbilityController controller)
        {
            base.InitAbility(controller);
            deltaTimer = GetResource.DeltaTimer();
            movementController = controller.GetComponent<PlayerMovementController>();
            isPressingMovementKeys = Resources.Load("Player/Movement/Player - Is Pressing Movement Keys") as ScriptableBool;
            hitboxController = controller.GetComponent<PlayerController>().hitboxController;
            animController = controller.GetComponent<PlayerAnimationController>();
            movementInput = Resources.Load("Player/Movement/Movement Inputs") as PlayerMovementInputs;
            isInAction = Resources.Load("Player/Actions/Is Player In Action") as ScriptableBool;
        }

        public override void StartAbilityUsage()
        {
            base.StartAbilityUsage();

            if (isPressingMovementKeys.value == false)
                return;

            isInAction.value = true;
            GetRollDirection();
            GetTargetRollPosition();
            GetObstacleRayDirection();
            PlayRollAnimation();
            hitboxController.DisableHitboxes();
            abilityController.StartCoroutine(StartRollTimer());
        }

        public override void StopAbilityUsage()
        {
            base.StopAbilityUsage();
        }

        public override void TickAbility()
        {
            base.TickAbility();

        }

        public override void FixedTickAbility()
        {
            base.FixedTickAbility();
            abilityController.transform.position = Vector3.MoveTowards(abilityController.transform.position, targetRollPosition, rollSpeed * Time.fixedDeltaTime);
            CheckForCollisions();
        }

        IEnumerator StartRollTimer()
        {
            yield return new WaitForSeconds(rollDuration);
            EndRoll();
        }

        void CheckForCollisions()
        {
            if (Physics.Raycast(new Vector3(abilityController.transform.position.x, abilityController.transform.position.y + 0.5f, abilityController.transform.position.z), rayDirection, out rayHit, obstacleDistanceCheck, obstaclesLayerMask.value))
            {
                EndRoll();
            }
        }

        void EndRoll()
        {
            isInAction.value = false;
            hitboxController.EnableHitboxes();
            animController.SetAnimBool(animController.playerAnimData.isRollingBool, false);
            StopAbilityUsage();
        }

        void GetRollDirection()
        {
            if (movementInput.backwardsMovementInput.value == true && movementInput.leftMovementInput.value == true)
            {
                currentRollDirection = RollDirection.BOTTOM_LEFT;
                return;
            }

            if (movementInput.backwardsMovementInput.value == true && movementInput.rightMovementInput.value == true)
            {
                currentRollDirection = RollDirection.BOTTOM_RIGHT;
                return;
            }

            if (movementInput.forwardMovementInput.value == true && movementInput.leftMovementInput.value == true)
            {
                currentRollDirection = RollDirection.TOP_LEFT;
                return;
            }

            if (movementInput.forwardMovementInput.value == true && movementInput.rightMovementInput.value == true)
            {
                currentRollDirection = RollDirection.TOP_RIGHT;
                return;
            }

            if (movementInput.forwardMovementInput.value == true)
            {
                currentRollDirection = RollDirection.TOP;
                return;
            }

            if (movementInput.backwardsMovementInput.value == true)
            {
                currentRollDirection = RollDirection.BOTTOM;
                return;
            }

            if (movementInput.leftMovementInput.value == true)
            {
                currentRollDirection = RollDirection.LEFT;
            }

            if (movementInput.rightMovementInput.value == true)
            {
                currentRollDirection = RollDirection.RIGHT;
            }
        }

        void GetTargetRollPosition()
        {
            switch (currentRollDirection)
            {
                case RollDirection.LEFT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x - rollDistance, abilityController.transform.position.y, abilityController.transform.position.z);
                    break;
                case RollDirection.RIGHT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x + rollDistance, abilityController.transform.position.y, abilityController.transform.position.z);
                    break;
                case RollDirection.TOP:
                    targetRollPosition = new Vector3(abilityController.transform.position.x, abilityController.transform.position.y, abilityController.transform.position.z + rollDistance);
                    break;
                case RollDirection.BOTTOM:
                    targetRollPosition = new Vector3(abilityController.transform.position.x, abilityController.transform.position.y, abilityController.transform.position.z - rollDistance);
                    break;
                case RollDirection.TOP_LEFT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x - rollDistance, abilityController.transform.position.y, abilityController.transform.position.z + rollDistance);
                    break;
                case RollDirection.TOP_RIGHT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x + rollDistance, abilityController.transform.position.y, abilityController.transform.position.z + rollDistance);
                    break;
                case RollDirection.BOTTOM_LEFT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x - rollDistance, abilityController.transform.position.y, abilityController.transform.position.z - rollDistance);
                    break;
                case RollDirection.BOTTOM_RIGHT:
                    targetRollPosition = new Vector3(abilityController.transform.position.x + rollDistance, abilityController.transform.position.y, abilityController.transform.position.z - rollDistance);
                    break;

            }
        }

        void GetObstacleRayDirection()
        {
            rayDirection = (targetRollPosition - abilityController.transform.position).normalized;
        }

        void PlayRollAnimation()
        {
            animController.SetAnimBool(animController.playerAnimData.isRollingBool, true);
            animController.PlayAnimationHash(animController.playerAnimData.slideForwardAnimClass);
        }
    }
}