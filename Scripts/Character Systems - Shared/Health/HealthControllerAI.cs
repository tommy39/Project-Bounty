using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Animations;
using IND.Core.AISystems;
using IND.Core.AISystems.Movement;
using IND.Core.AISystems.Inventory;
using IND.Core.Characters.LimbGibbings;
using IND.Core.Weapons;
using IND.Core.Characters.Hitboxes;
using IND.Core.AISystems.TroopSystems;
using IND.Core.WorldInteractions;
using IND.Core.Shared.Statics;

namespace IND.Core.Characters.Health
{
    public class HealthControllerAI : HealthController
    {
        [HideInInspector] public HitboxController hitboxController;
        [HideInInspector] public LimbsController limbsController;
        [HideInInspector] public AIKnockDownController knockdownController;
        AIController aiController;
        AIMovementController movementController;
        AnimationController animController;
        AIInventoryController inventoryController;
        AIManager aiManager;

        [Required] public GameObject doorCollider;
        [Required] public GameObject notificationCollider;

        public override void Init()
        {
            base.Init();

            aiController = GetComponent<AIController>();
            movementController = GetComponent<AIMovementController>();
            animController = GetComponent<AnimationController>();
            inventoryController = GetComponent<AIInventoryController>();
            hitboxController = GetComponentInChildren<HitboxController>();
            limbsController = GetComponentInChildren<LimbsController>();
            knockdownController = GetComponent<AIKnockDownController>();
            aiManager = aiController.aiManager;

        }

        public override void Tick()
        {
            base.Tick();


        }
        public override void TakeDamage(int amount, HitboxType hitboxType)
        {
            base.TakeDamage(amount, hitboxType);
        }

        public override void Death(HitboxType hitboxType)
        {
            base.Death(hitboxType);
            aiController.aiManager.OnDeadAI(aiController);
            hitboxController.DisableHitboxes();
            limbsController.OnLimbHit(hitboxType);
            animController.PlayAnimationHash(animController.animData.death01AnimationClass);
            movementController.OnDeath();
            animController.animHook.enabled = false;
            inventoryController.DropCurrentInHandWeapon(false);
            doorCollider.SetActive(false);
            notificationCollider.SetActive(false);
            Destroy(aiController.fieldOfViewController);
            Destroy(aiController.searchController);
            Destroy(aiController.notificationHandler);
        }
    }
}