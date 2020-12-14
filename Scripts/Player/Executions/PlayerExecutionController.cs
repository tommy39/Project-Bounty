using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.AISystems;
using IND.Core.Player.Inventory;
using Sirenix.OdinInspector;
using IND.Core.Characters.Animations;


namespace IND.Core.Player.Executions
{
    public class PlayerExecutionController : IND_Mono
    {
        protected PlayerInventoryController inventoryController;
        private PlayerAnimationController playerAnimController;

        [ShowIf("aiThatPlayerIsOnTopOf")] [SerializeField] private AIKnockDownController aiThatPlayerIsOnTopOf;
        [FoldoutGroup("References")] [SerializeField] [InlineEditor] [Required] protected ScriptableBool isPlayerInAction;
        [FoldoutGroup("References")] [SerializeField] [InlineEditor] [Required] protected ScriptableBool leftClickInput;
        [FoldoutGroup("References")] [Required] [InlineEditor] public PlayerExecutionData executionData;
        private Collider[] hitColliders;
        private Collider nearestCollider;

        protected bool isCurrentlyExecuting;
        private float currentShortestDistance;
        private float distanceBetweenObjectsInLoop;
        public override void Init()
        {
            inventoryController = GetComponent<PlayerInventoryController>();
            playerAnimController = GetComponent<PlayerAnimationController>();

            isPlayerInAction.value = false;
        }

        public override void Tick()
        {
            if (executionData.executionInput.value == true)
            {
                if (aiThatPlayerIsOnTopOf != null) //Is already in executionMode
                {
                    ExitCurrentAiThatPlayerIsOnTopOf();
                }
                else
                {
                    CheckForNearbyEnemiesThatAreKnockedDown();
                }
            }

            if (leftClickInput.value == true)
            {
                if (aiThatPlayerIsOnTopOf != null)
                {
                    ExecuteAITarget();
                }
            }
        }

        void ExecuteAITarget()
        {
            aiThatPlayerIsOnTopOf.healthController.TakeDamage(1000, Characters.Hitboxes.HitboxType.NULL);
            isCurrentlyExecuting = true;
            playerAnimController.PlayAnimationHash(playerAnimController.playerAnimData.executionSlashAnimClass);
            aiThatPlayerIsOnTopOf = null;
        }

        public void EndExecution()
        {
            isCurrentlyExecuting = false;
            isPlayerInAction.value = false;
            playerAnimController.SetAnimBool(playerAnimController.playerAnimData.isInActionBool, false);
        }

        void ExitCurrentAiThatPlayerIsOnTopOf()
        {
            aiThatPlayerIsOnTopOf.playerIsOnTop = false;
            aiThatPlayerIsOnTopOf = null;
            isPlayerInAction.value = false;
            isCurrentlyExecuting = false;
        }

        void CheckForNearbyEnemiesThatAreKnockedDown()
        {
            if (isPlayerInAction.value == true)
                return;

            hitColliders = Physics.OverlapSphere(transform.position, executionData.maxSearchDistance, executionData.knockedDownEnemiesLayer.value);

            aiThatPlayerIsOnTopOf = GetNearestAI();
            if (aiThatPlayerIsOnTopOf != null)
            {
                MountTargetAI();
            }
        }

        void MountTargetAI()
        {
            aiThatPlayerIsOnTopOf.playerIsOnTop = true;
            isPlayerInAction.value = true;
            transform.position = aiThatPlayerIsOnTopOf.mountPositionForPlayerWhenKnockedOut.position;
            playerAnimController.SetAnimBool(playerAnimController.playerAnimData.isInActionBool, true);
            playerAnimController.PlayAnimationHash(playerAnimController.playerAnimData.executionCrouchMountAnimClass);
            Debug.Log("Mount AI");
        }

        AIKnockDownController GetNearestAI()
        {
            currentShortestDistance = executionData.maxSearchDistance;
            nearestCollider = null;

            for (int i = 0; i < hitColliders.Length; i++)
            {
                distanceBetweenObjectsInLoop = Vector3.Distance(transform.position, hitColliders[i].transform.position);

                if (distanceBetweenObjectsInLoop < currentShortestDistance) //Is Shorter than the current distance
                {
                    nearestCollider = hitColliders[i];
                    currentShortestDistance = distanceBetweenObjectsInLoop;
                }
            }

            if (nearestCollider != null)
            {
                return nearestCollider.GetComponentInParent<AIKnockDownController>();
            }
            else
            {
                return null;
            }
        }
    }
}