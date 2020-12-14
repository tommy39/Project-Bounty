using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Inventory;
using IND.Core.Characters.Animations;
using IND.Core.Weapons;
using IND.Core.Player;
using IND.Core.Player.Combat;
using IND.Core.UI.Weapons.Reload;

namespace IND.Core.Player.Weapons
{
    public class PlayerReloadController : IND_Mono
    {
        public float reloadSpeed = 1f;

        private PlayerWeaponController weaponController;
        private PlayerInventoryController inventoryController;
        private PlayerAnimationController animController;
        private UI_ReloadProgressBar uiReloadProgressBar;

        [SerializeField] protected ScriptableBool useUnlimitedAmmo;
        private ScriptableInputBoolAction reloadInput;
        private ScriptableBool isReloading;
        public override void Init()
        {
            weaponController = GetComponent<PlayerWeaponController>();
            inventoryController = GetComponent<PlayerInventoryController>();
            animController = GetComponent<PlayerAnimationController>();
            uiReloadProgressBar = FindObjectOfType<UI_ReloadProgressBar>();

            isReloading = Resources.Load("Player/PlayerIsReloading") as ScriptableBool;
            isReloading.value = false;

            reloadInput = Resources.Load("Input Actions/Input Action Reload") as ScriptableInputBoolAction;
        }

        public override void Tick()
        {
            if (weaponController.currentWeaponType != WeaponType.RANGED)
                return;

            if(reloadInput.value.value)
            {
                if (isReloading.value)
                    return;

                StartReload();
            }
        }

        void StartReload()
        {
            if (weaponController.rangedWeaponItemRuntime.currentAmmoInMagazine == weaponController.rangedWeaponItemRuntime.rangedData.maxMagazineAmmoAmount)
                return;


            switch (weaponController.rangedWeaponItemRuntime.rangedData.ammoType)
            {
                case WeaponRangedAmmoType.RIFLE_AMMO:
                    if (inventoryController.inventoryAmmo.rifleAmmo == 0)
                    {
                        return;
                    }           
                    break;
                case WeaponRangedAmmoType.PISTOL_AMMO:
                    if (inventoryController.inventoryAmmo.pistolAmmo == 0)
                    {
                        return;
                    }
                    break;
                case WeaponRangedAmmoType.SHOTGUN_SHELLS:
                    if (inventoryController.inventoryAmmo.shotgunShells == 0)
                    {
                        return;
                    }
                    break;
                case WeaponRangedAmmoType.ARROWS:
                    if (inventoryController.inventoryAmmo.arrows == 0)
                    {
                        return;
                    }
                    break;
            }

            isReloading.value = true;
            animController.SetAnimBool(animController.animData.isReloadingBool, true);
            uiReloadProgressBar.ActivateNewReload(weaponController.rangedWeaponItemRuntime.rangedData, reloadSpeed);
            weaponController.rangedWeaponItemRuntime.audioSource.PlayOneShot(weaponController.rangedWeaponItemRuntime.rangedData.reloadSound);
        }

        public void ReloadCompleted()
        {
            int reloadAmount = 0;
            int currentAmmoInMagazine = weaponController.rangedWeaponItemRuntime.currentAmmoInMagazine;
            int maxReloadAmount = weaponController.rangedWeaponItemRuntime.rangedData.maxMagazineAmmoAmount;

            switch (weaponController.rangedWeaponItemRuntime.rangedData.ammoType)
            {
                case WeaponRangedAmmoType.RIFLE_AMMO:
                    if (inventoryController.inventoryAmmo.rifleAmmo > maxReloadAmount)
                    {
                        reloadAmount = maxReloadAmount;
                    }
                    else
                    {
                        reloadAmount = inventoryController.inventoryAmmo.rifleAmmo;
                    }
                    break;
                case WeaponRangedAmmoType.PISTOL_AMMO:
                    if (inventoryController.inventoryAmmo.pistolAmmo > maxReloadAmount)
                    {
                        reloadAmount = maxReloadAmount;
                    }
                    else
                    {
                        reloadAmount = inventoryController.inventoryAmmo.pistolAmmo;
                    }
                    break;
                case WeaponRangedAmmoType.SHOTGUN_SHELLS:
                    if (inventoryController.inventoryAmmo.shotgunShells > maxReloadAmount)
                    {
                        reloadAmount = maxReloadAmount;
                    }
                    else
                    {
                        reloadAmount = inventoryController.inventoryAmmo.shotgunShells;
                    }
                    break;
                case WeaponRangedAmmoType.ARROWS:
                    if (inventoryController.inventoryAmmo.arrows > maxReloadAmount)
                    {
                        reloadAmount = maxReloadAmount;
                    }
                    else
                    {
                        reloadAmount = inventoryController.inventoryAmmo.arrows;
                    }
                    break;
            }

            if (useUnlimitedAmmo.value == true)
            {
                reloadAmount = maxReloadAmount;
            }

            reloadAmount -= currentAmmoInMagazine;

            weaponController.rangedWeaponItemRuntime.currentAmmoInMagazine += reloadAmount;

            if (useUnlimitedAmmo.value == false)
            {
                switch (weaponController.rangedWeaponItemRuntime.rangedData.ammoType)
                {
                    case WeaponRangedAmmoType.RIFLE_AMMO:
                        inventoryController.inventoryAmmo.rifleAmmo -= reloadAmount;
                        break;
                    case WeaponRangedAmmoType.PISTOL_AMMO:
                        inventoryController.inventoryAmmo.pistolAmmo -= reloadAmount;
                        break;
                    case WeaponRangedAmmoType.SHOTGUN_SHELLS:
                        inventoryController.inventoryAmmo.shotgunShells -= reloadAmount;
                        break;
                    case WeaponRangedAmmoType.ARROWS:
                        inventoryController.inventoryAmmo.arrows -= reloadAmount;
                        break;
                }
            }
            isReloading.value = false;
            animController.SetAnimBool(animController.animData.isReloadingBool, false);
            weaponController.rangedWeaponItemRuntime.FinishReload();
        }

        public void ReloadCancelled()
        {
            isReloading.value = false;
            animController.SetAnimBool(animController.animData.isReloadingBool, false);
            uiReloadProgressBar.OnReloadCancelled();
        }
    }
}