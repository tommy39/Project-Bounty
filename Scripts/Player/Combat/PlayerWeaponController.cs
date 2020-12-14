using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Player.Weapons;
using IND.Core.Weapons.Grenades;
using IND.Core.WorldInteractions;
using IND.Core.Characters.Animations;
using IND.Core.UI.Weapons.Reload;
using IND.Core.Characters;
using IND.Core.Player.Combat;

namespace IND.Core.Player.Combat
{
    public class PlayerWeaponController : IND_Mono
    {
        [InlineEditor] public WeaponItemRuntime currentRuntimeWeapon;
        [HideInInspector] public WeaponType currentWeaponType;
        [HideInInspector] public WeaponItemRuntimeMelee meleeWeaponItemRuntime;
        [HideInInspector] public WeaponItemRuntimeRanged rangedWeaponItemRuntime;


        [Required] public Transform grenadeThrowPosition;
        [Required] public Transform bulletShootPoint;

        [HideInInspector] public Transform aimMouseTarget;

        private PlayerAnimationController animController;
        private ScriptableInputBoolAction attackInputAction;
        private ScriptableInputBoolAction attackInputHeldAction;

        private ScriptableBool isPlayerReloading;

        public override void Init()
        {
            animController = GetComponent<PlayerAnimationController>();
            attackInputAction = Resources.Load("Input Actions/Input Action Attack") as ScriptableInputBoolAction;
            attackInputHeldAction = Resources.Load("Input Actions/Input Action Attack Held") as ScriptableInputBoolAction;
            isPlayerReloading = Resources.Load("Player/PlayerIsReloading") as ScriptableBool;
        }

        public override void Tick()
        {
            if (currentWeaponType == WeaponType.RANGED)
            {
                if (rangedWeaponItemRuntime.rangedData.fireRateMode == WeaponRangedFireRateMode.FULL_AUTO)
                {
                    if (attackInputHeldAction.value.value)
                    {
                        HandleWeaponAttack();
                    }
                }
                else
                {
                    if (attackInputAction.value.value)
                    {
                        HandleWeaponAttack();
                    }
                }
            }
            else
            {
                if (attackInputAction.value.value)
                {
                    HandleWeaponAttack();
                }
            }
        }

        public void OnWeaponEquipped(WeaponItemRuntime weapon)
        {
            currentWeaponType = weapon.weaponItemData.weaponType;
            switch (currentWeaponType)
            {
                case WeaponType.RANGED:
                    rangedWeaponItemRuntime = weapon as WeaponItemRuntimeRanged;
                    break;
                case WeaponType.MELEE:
                    meleeWeaponItemRuntime = weapon as WeaponItemRuntimeMelee;
                    break;
                case WeaponType.THROWABLE:
                    break;
                case WeaponType.FISTS:
                    meleeWeaponItemRuntime = weapon as WeaponItemRuntimeMelee;
                    break;
            }

            currentRuntimeWeapon = weapon;
            animController.SetAnimInt(animController.animData.weaponAnimID, weapon.weaponItemData.weaponAnimID);
            animController.PlayAnimationHash(animController.animData.getNewWeaponAnimationClass);
        }

        void HandleWeaponAttack()
        {
            switch (currentWeaponType)
            {
                case WeaponType.RANGED:
                    HandleRangedWeaponAttack();
                    break;
                case WeaponType.MELEE:
                    meleeWeaponItemRuntime.StartMeleeAttack();
                    break;
                case WeaponType.THROWABLE:
                    break;
                case WeaponType.FISTS:
                    meleeWeaponItemRuntime.StartMeleeAttack();
                    break;
            }
        }

        void HandleRangedWeaponAttack()
        {
            if (rangedWeaponItemRuntime.canAttack == false)
                return;

            if (rangedWeaponItemRuntime.currentAmmoInMagazine == 0)
            {
                rangedWeaponItemRuntime.audioSource.PlayOneShot(rangedWeaponItemRuntime.rangedData.magazineOutOfAmmoSound);
                return;
            }
            if (isPlayerReloading.value)
                return;

            rangedWeaponItemRuntime.FireGunShared();
            rangedWeaponItemRuntime.canAttack = false;
            rangedWeaponItemRuntime.StartCoroutine("AttackCooldownCoroutine");
        }

    }
}