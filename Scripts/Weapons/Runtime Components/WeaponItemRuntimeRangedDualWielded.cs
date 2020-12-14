using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;
using IND.Core.Pooling;
using IND.Core.Player.Inventory;
using IND.Core.WorldInteractions;

namespace IND.Core.Weapons
{
    public class WeaponItemRuntimeRangedDualWielded : WeaponItemRuntimeRanged
    {
        private WeaponItemRanged_DualWielded dualWieldData;
        private WeaponItemRuntimeRanged leftHandWeapon;
        private WeaponItemRuntimeRanged rightHandWeapon;

        public bool usingRightHand = false;

        public override void PickupWeaponPlayer()
        {
            base.PickupWeaponPlayer();
        }

        public void SetupDualWield(WeaponItemRanged_DualWielded data, WeaponItemRuntimeRanged lhWeapon, WeaponItemRuntimeRanged rhWeapon)
        {
            dualWieldData = data;
            rangedData = data;
            weaponItemData = data;
            leftHandWeapon = lhWeapon;
            rightHandWeapon = rhWeapon;
            PickupWeaponPlayer();

            isDualWielded = true;
            leftHandWeapon.isDualWielded = true;
            rightHandWeapon.isDualWielded = true;

            currentAmmoInMagazine = lhWeapon.currentAmmoInMagazine + rhWeapon.currentAmmoInMagazine;
        }   

        public void DestroyDualWield()
        {
            leftHandWeapon.isDualWielded = false;
            rightHandWeapon.isDualWielded = false;

            DropLeftHandWeapon();
            DropRightHandWeapon();

            Destroy(this);
        }

        public void OnWeaponEquipped()
        {
            leftHandWeapon.gameObject.SetActive(true);
            rightHandWeapon.gameObject.SetActive(true);
        }

        void DropLeftHandWeapon()
        {
            WorldInteractionEvent_PickupWeaponRuntime pickupWeaponInteraction = WeaponStaticUtils.DropWeaponToGround(leftHandWeapon, playerInventoryController.worldInteractionParentGroup, playerInventoryController.searcherController.controllerData.worldInteractionPrefab);
            leftHandWeapon.OnDropWeaponToGround();
            WeaponStaticUtils.ThrowWeaponWithForce(pickupWeaponInteraction, leftHandWeapon.weaponItemData.throwDistanceForce, playerInventoryController.transform.forward);
        }

        void DropRightHandWeapon()
        {
            WorldInteractionEvent_PickupWeaponRuntime pickupWeaponInteraction = WeaponStaticUtils.DropWeaponToGround(rightHandWeapon, playerInventoryController.worldInteractionParentGroup, playerInventoryController.searcherController.controllerData.worldInteractionPrefab);
            rightHandWeapon.OnDropWeaponToGround();
            WeaponStaticUtils.ThrowWeaponWithForce(pickupWeaponInteraction, rightHandWeapon.weaponItemData.throwDistanceForce, playerInventoryController.transform.forward);
        }

        public override void Init()
        {           

        }

        public override void FreshSpawnWeapon()
        {
            base.FreshSpawnWeapon();
        }

        public override void FinishReload()
        {
            base.FinishReload();

            leftHandWeapon.currentAmmoInMagazine = currentAmmoInMagazine / 2;
            rightHandWeapon.currentAmmoInMagazine = currentAmmoInMagazine / 2;
        }
        public override void FireGunShared()
        {
            if (usingRightHand == true)
            {
                rightHandWeapon.currentAmmoInMagazine--;
                usingRightHand = false;
            }
            else
            {
                leftHandWeapon.currentAmmoInMagazine--;
                usingRightHand = true;
            }
           
            currentAmmoInMagazine--;
            animController.SetAnimBool(animController.animData.isFiringBool, true);
            GameObject bulletCasing = Instantiate(rangedData.bulletCasingPrefab);
            if (usingRightHand == true)
            {
                rightHandWeapon.muzzleFlash.PlayParticle();
                bulletCasing.transform.position = rightHandWeapon.bulletEjectionPort.position;
                bulletCasing.transform.rotation = rightHandWeapon.bulletEjectionPort.rotation;
                rightHandWeapon.audioSource.PlayOneShot(rightHandWeapon.rangedData.fireShotSound, 0.5f);

            }
            else
            {
                leftHandWeapon.muzzleFlash.PlayParticle();
                bulletCasing.transform.position = leftHandWeapon.bulletEjectionPort.position;
                bulletCasing.transform.rotation = leftHandWeapon.bulletEjectionPort.rotation;
                leftHandWeapon.audioSource.PlayOneShot(leftHandWeapon.rangedData.fireShotSound, 0.5f);

            }

            switch (rangedData.shootType)
            {
                case WeaponBulletType.SINGLE_BULLET:
                    FireSingleBullet();
                    break;
                case WeaponBulletType.SPREAD_BULLET:
                    FireSpreadBullet();
                    break;         
            }
        }

        public override void FireSingleBullet()
        {
            base.FireSingleBullet();
        }

        public override void FireSpreadBullet()
        {
            base.FireSpreadBullet();
        }

        public override void WeaponAttack()
        {
            base.WeaponAttack();

        }
        public override IEnumerator AttackCooldownCoroutine()
        {
            return base.AttackCooldownCoroutine();

        }

        public override void AttackCooldownEnd()
        {
            base.AttackCooldownEnd();

        }


    }
}