using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.UI.Weapons.Reload;
using IND.Core.Player.Combat;
using IND.Core.Weapons;
using IND.Core.Characters.Health;
using IND.Core.Player.Executions;

namespace IND.Core.Characters.Animations
{
    public class AnimatorHookPlayer : AnimatorHook
    {
        private PlayerWeaponController playerWeaponController;
        private HealthControllerPlayer healthControllerPlayer;
        private PlayerExecutionController executionController;
        public override void Init()
        {
            base.Init();

            healthControllerPlayer = GetComponentInParent<HealthControllerPlayer>();
            playerWeaponController = GetComponentInParent<PlayerWeaponController>();
            executionController = GetComponentInParent<PlayerExecutionController>();
        }

       

        public override void OpenMeleeAttackColliders()
        {
            playerWeaponController.meleeWeaponItemRuntime.EnableAttackColliders();
        }

        public override void CloseMeleeAttackColliders()
        {
            playerWeaponController.meleeWeaponItemRuntime.DisableAttackColliders();
        }


        public override void OpenFistsAttackColliders()
        {
            playerWeaponController.meleeWeaponItemRuntime.EnableAttackColliders();
        }

        public override void CloseFistsAttackColliders()
        {
            playerWeaponController.meleeWeaponItemRuntime.DisableAttackColliders();
        }
        public override void EndFistsAttack()
        {
            animController.SetAnimBool(animController.animData.isMeleeAttackingBool, false);
            playerWeaponController.meleeWeaponItemRuntime.canAttack = true;
        }
        public override void EndMeleeAttack()
        {
            animController.SetAnimBool(animController.animData.isMeleeAttackingBool, false);
            playerWeaponController.meleeWeaponItemRuntime.canAttack = true;
        }

        public override void EnableRagdoll()
        {
            base.EnableRagdoll();
            healthControllerPlayer.playerCollision.enabled = false;
        }

        public void EndExecution()
        {
            executionController.EndExecution();
        }
    }
}