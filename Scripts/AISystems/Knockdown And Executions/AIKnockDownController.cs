using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using IND.Core.AISystems.Movement;
using IND.Core.Characters.Animations;
using IND.Core.AISystems.Inventory;
using IND.Core.AISystems.TroopSystems;
using IND.Core.Weapons;
using IND.Core.Characters.Health;
using IND.Core.WorldInteractions;
using IND.Core.Shared.Statics;
using IND.Core.AISystems.States;

namespace IND.Core.AISystems
{
    public class AIKnockDownController : IND_Mono
    {
        HitboxController hitboxController;
        LimbsController limbsController;
        AIMovementController movementController;
        AnimationController animController;
        AIInventoryController inventoryController;
        AITroopController troopController;
        AIStateController stateController;
        [HideInInspector] public HealthControllerAI healthController;

        public bool playerIsOnTop = false;
        [Required] [SerializeField] protected Collider knockDownNotificationCollider;
        [Required] public Transform mountPositionForPlayerWhenKnockedOut;
        public float disableAnimatorOnKnockdownTimer = 1.7f;

        [Title("Knockdown Variables")] public bool isKnockedDown = false;
        public bool isRecoveringFromKnockDown = false;

        [ShowIf("isRecoveringFromKnockDown")] public WeaponItemRuntime droppedWeapon;

        private Vector3 targetPositionForWeaponPickup;
        private float currentDistanceFromWeaponPickup;
        public override void Init()
        {
            movementController = GetComponent<AIMovementController>();
            animController = GetComponent<AnimationController>();
            inventoryController = GetComponent<AIInventoryController>();
            hitboxController = GetComponentInChildren<HitboxController>();
            limbsController = GetComponentInChildren<LimbsController>();
            troopController = GetComponent<AITroopController>();
            healthController = GetComponent<HealthControllerAI>();
            stateController = GetComponent<AIStateController>();

            knockDownNotificationCollider.enabled = false;
        }

        public override void Tick()
        {
            if (isRecoveringFromKnockDown == true)
            {
                CheckDistanceFromDroppedWeapon();
            }
        }

        public void ApplyKnockdown()
        {
            if (isKnockedDown)
                return;

            isKnockedDown = true;
            StartCoroutine(KnockdownTimer());

            animController.SetAnimBool(animController.animData.isKnockedDown, true);
            animController.PlayAnimationHash(animController.animData.knockDownAnimationClass);
            limbsController.EnableEntireRagdoll();
            hitboxController.DisableHitboxes();
            healthController.doorCollider.SetActive(false);
            healthController.notificationCollider.SetActive(false);

            if (inventoryController.currentWeaponRuntime != null)
            {
                droppedWeapon = inventoryController.currentWeaponRuntime;
            }

            inventoryController.DropCurrentInHandWeapon(false);
            StartCoroutine(DisableAnimatorTimer());

            knockDownNotificationCollider.enabled = true;
            stateController.ChangeState(AIStateType.KNOCKEDDOWN);
        }

        void WakeUpFromKnockdown()
        {
            isKnockedDown = false;
            isRecoveringFromKnockDown = true;

            hitboxController.EnableHitboxes();
            limbsController.DisableEntireRagdoll();
            healthController.doorCollider.SetActive(true);
            healthController.notificationCollider.SetActive(true);
            animController.animHook.enabled = true;
            animController.SetAnimInt(animController.animData.weaponAnimID, 0);
            animController.PlayAnimationHash(animController.animData.getNewWeaponAnimationClass);
            animController.SetAnimBool(animController.animData.isKnockedDown, false);
            StartCoroutine(FetchWeaponAfterGetUpTimer());

            knockDownNotificationCollider.enabled = false;
        }

        void EndKnockDownAfterWeaponFound()
        {
            isRecoveringFromKnockDown = false;
            droppedWeapon.GetComponentInParent<WorldInteractionController>().DestroyInteractionOnly(droppedWeapon.gameObject);
            inventoryController.EquipExistingWeapon(droppedWeapon);
            droppedWeapon = null;

            stateController.ChangeState(AIStateType.COMBAT);
            stateController.combatState.target = FindObjectOfType<HealthControllerPlayer>();
        }

        void CheckDistanceFromDroppedWeapon()
        {
            float curDistance = Vector3.Distance(transform.position, targetPositionForWeaponPickup);
            if (curDistance < 0.8f)
            {
                EndKnockDownAfterWeaponFound();
            }
        }

        IEnumerator DisableAnimatorTimer()
        {
            yield return new WaitForSeconds(disableAnimatorOnKnockdownTimer);
            animController.animHook.enabled = false;
        }

        IEnumerator KnockdownTimer()
        {
            yield return new WaitForSeconds(troopController.troopData.knockDownDuration);
            if (playerIsOnTop == true)
            {
                StartCoroutine(KnockdownTimer());
            }
            else
            {
                WakeUpFromKnockdown();
            }
        }

        IEnumerator FetchWeaponAfterGetUpTimer()
        {
            Debug.Log(gameObject.name);
            yield return new WaitForSeconds(1.8f);
            if (droppedWeapon.isPlayerWeapon == true)
            {
                droppedWeapon = null;

                Debug.Log("Player Took Weapon From " + gameObject.name);
            }
            else
            {
                targetPositionForWeaponPickup = GetNearestNavMeshPoint.GetClosestNavMeshPoint(droppedWeapon.transform.position);
                movementController.SetAgentDestination(targetPositionForWeaponPickup);
                animController.SetAnimBool(animController.animData.isIdleBool, true);
                animController.SetAnimFloat(animController.animData.verticalFloat, 1);
            }
        }
    }
}