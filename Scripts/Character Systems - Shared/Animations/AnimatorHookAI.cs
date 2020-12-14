using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.Inventory;

namespace IND.Core.Characters.Animations
{
    public class AnimatorHookAI : AnimatorHook
    {
        private AIInventoryController inventoryController;
        public override void Init()
        {
            base.Init();
            inventoryController = GetComponentInParent<AIInventoryController>();
        }

        public override void ReloadCompleted()
        {
            inventoryController.rangedWeaponRuntime.targetReloadAmount = inventoryController.rangedWeaponRuntime.rangedData.maxMagazineAmmoAmount;
            inventoryController.rangedWeaponRuntime.FinishReload();
        }

        public override void DropMagazineDuringReload()
        {

        }

        public override void OpenMeleeAttackColliders()
        {
            inventoryController.meleeWeaponRuntime.EnableAttackColliders();

        }

        public override void CloseMeleeAttackColliders()
        {
            inventoryController.meleeWeaponRuntime.DisableAttackColliders();
        }
        public override void EndMeleeAttack()
        {
            animController.SetAnimBool(animController.animData.isMeleeAttackingBool, false);
            inventoryController.meleeWeaponRuntime.canAttack = true;
        }
    }
}
