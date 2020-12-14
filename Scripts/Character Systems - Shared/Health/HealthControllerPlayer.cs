using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Animations;
using IND.Core.Player;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using IND.Core.Player.Inventory;
using IND.Core.UI.LevelRestart;

namespace IND.Core.Characters.Health
{
    public class HealthControllerPlayer : HealthController
    {
        private PlayerAnimationController animController;
        PlayerController playerController;
        PlayerInventoryController inventoryController;
        [HideInInspector] public HitboxController hitboxController;
        [HideInInspector] public LimbsController limbsController;

        [Required] public Collider playerCollision;

        [InlineEditor] [FoldoutGroup("Dev Settings")] public ScriptableBool unlimitedHealth;

        public override void Init()
        {
            base.Init();
            animController = GetComponent<PlayerAnimationController>();
            playerController = GetComponent<PlayerController>();
            hitboxController = GetComponentInChildren<HitboxController>();
            limbsController = GetComponentInChildren<LimbsController>();
            inventoryController = GetComponent<PlayerInventoryController>();

            if (unlimitedHealth.value)
            {
                currentHealth = 9999999;
            }
        }

        public override void Tick()
        {

        }

        public override void TakeDamage(int amount, HitboxType hitboxType)
        {
            base.TakeDamage(amount, hitboxType);
        }
        public override void Death(HitboxType hitboxType)
        {
            base.Death(hitboxType);
            playerController.tickPlayer.value = false;
            animController.PlayAnimationHash(animController.animData.death01AnimationClass);
            hitboxController.DisableHitboxes();
            animController.animHook.enabled = false;
            inventoryController.DropWeaponToGround(false, inventoryController.currentWeaponItemRuntime);
            playerController.cameraFocusPoint.SetParent(null);
            UI_LevelRestartManager.singleton.ActivateRestartMenu();
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.isKinematic = true;
            Destroy(playerController);
        }
    }
}