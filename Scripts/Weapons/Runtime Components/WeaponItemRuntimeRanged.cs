using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;
using IND.Core.Pooling;

namespace IND.Core.Weapons
{
    public class WeaponItemRuntimeRanged : WeaponItemRuntime
    {
        [HideInInspector] public WeaponItemRanged rangedData;
        public int currentAmmoInMagazine;

        [Required] public WeaponMuzzleFlashController muzzleFlash;
        [InfoBox("The Z direction is the direction that the bullet ejects")] [Required] public Transform bulletEjectionPort;

        [HideInInspector] public int targetReloadAmount;

        [HideInInspector] public bool isReloading = false;
        public bool isDualWielded = false;
        public Transform pivot;
        private float distanceModifier;
        [HideInInspector] public AudioSource audioSource;

        public override void PickupWeaponPlayer()
        {
            base.PickupWeaponPlayer();
            audioSource = GetComponent<AudioSource>();
        }

        public override void Init()
        {
            base.Init();
        }

        public override void FreshSpawnWeapon()
        {
            base.FreshSpawnWeapon();
            rangedData = weaponItemData as WeaponItemRanged;
            muzzleFlash.Init();
            currentAmmoInMagazine = rangedData.maxMagazineAmmoAmount;
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void FinishReload()
        {
            isReloading = false;
            canAttack = true;
            animController.SetAnimBool(animController.animData.isReloadingBool, false);
        }
        public virtual void FireGunShared()
        {
            currentAmmoInMagazine--;
            muzzleFlash.PlayParticle();
            animController.SetAnimBool(animController.animData.isFiringBool, true);
            GameObject bulletCasing = Instantiate(rangedData.bulletCasingPrefab);
            bulletCasing.transform.position = bulletEjectionPort.position;
            bulletCasing.transform.rotation = bulletEjectionPort.rotation;
            audioSource.PlayOneShot(rangedData.fireShotSound);

            switch (rangedData.shootType)
            {
                case WeaponBulletType.SINGLE_BULLET:
                    FireSingleBullet();
                    break;
                case WeaponBulletType.SPREAD_BULLET:
                    FireSpreadBullet();
                    break;
                case WeaponBulletType.ROCKET:
                    break;
                case WeaponBulletType.SPREAD_BOLT:
                    FireSpreadBolt();
                    break;
                case WeaponBulletType.SINGLE_BOLT:
                    FireSingleBolt();
                    break;
            }
        }

        public virtual void FireSingleBullet()
        {
            GameObject bulletToFire = PoolingManager.singleton.bulletPooler.GetPoolableObject();
            Weapon_FiredProjectile bulletComponent = bulletToFire.GetComponent<Weapon_FiredProjectile>();
            if (isPlayerWeapon)
            {
                bulletToFire.transform.position = playerWeaponController.bulletShootPoint.position;
                bulletToFire.transform.rotation = playerWeaponController.bulletShootPoint.rotation;
                bulletComponent.Init(rangedData.bulletMovementSpeed, rangedData.sharedWeaponData.layersPlayerCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);

                //Create GunShot  Notification
                CreateGunShotNotification();
            }
            else
            {
                bulletToFire.transform.position = aiInventoryController.bulletShootPoint.position;
                bulletToFire.transform.rotation = aiInventoryController.bulletShootPoint.rotation;

                bulletComponent.Init(rangedData.bulletMovementSpeed - rangedData.aiData.bulletSpeedModifier, rangedData.sharedWeaponData.layersEnemiesCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);
            }

            Weapon_BulletManager.singleton.bulletsToTick.Add(bulletComponent);
        }

        public virtual void FireSpreadBullet()
        {
            float angleSizePerShot = rangedData.spreadMaxRange / rangedData.spreadAmount;

            for (int i = 0; i < rangedData.spreadAmount; i++)
            {
                GameObject bulletToFire = PoolingManager.singleton.bulletPooler.GetPoolableObject();
                Weapon_FiredProjectile bulletComponent = bulletToFire.GetComponent<Weapon_FiredProjectile>();
                Vector3 rotationVal;
                if (isPlayerWeapon)
                {
                    bulletToFire.transform.position = playerWeaponController.bulletShootPoint.position;
                    rotationVal = playerWeaponController.bulletShootPoint.rotation.eulerAngles;
                    bulletComponent.Init(rangedData.bulletMovementSpeed, rangedData.sharedWeaponData.layersPlayerCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);

                    //Create GunShot  Notification
                    CreateGunShotNotification();
                }
                else
                {
                    bulletToFire.transform.position = aiInventoryController.bulletShootPoint.position;
                    rotationVal = aiInventoryController.bulletShootPoint.rotation.eulerAngles;

                    bulletComponent.Init(rangedData.bulletMovementSpeed - rangedData.aiData.bulletSpeedModifier, rangedData.sharedWeaponData.layersEnemiesCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);


                }
                Weapon_BulletManager.singleton.bulletsToTick.Add(bulletComponent);

                float curAngle = (rotationVal.y - rangedData.spreadMaxRange / 2 + angleSizePerShot * i);
                bulletToFire.transform.rotation = Quaternion.Euler(bulletToFire.transform.rotation.x, curAngle, bulletToFire.transform.rotation.z);
            }
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        void FireSpreadBolt()
        {
            Debug.Log("Fire Spread Bolt");
        }

        void FireSingleBolt()
        {

            GameObject bulletToFire = PoolingManager.singleton.arrowPooler.GetPoolableObject();
            Weapon_FiredProjectile_Arrow arrowComponent = bulletToFire.GetComponent<Weapon_FiredProjectile_Arrow>();
            if (isPlayerWeapon)
            {
                bulletToFire.transform.position = playerWeaponController.bulletShootPoint.position;
                bulletToFire.transform.rotation = playerWeaponController.bulletShootPoint.rotation;
                arrowComponent.Init(rangedData.bulletMovementSpeed, rangedData.sharedWeaponData.layersPlayerCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);

                //Create GunShot  Notification
                CreateGunShotNotification();
            }
            else
            {
                bulletToFire.transform.position = aiInventoryController.bulletShootPoint.position;
                bulletToFire.transform.rotation = aiInventoryController.bulletShootPoint.rotation;

                arrowComponent.Init(rangedData.bulletMovementSpeed - rangedData.aiData.bulletSpeedModifier, rangedData.sharedWeaponData.layersEnemiesCanHitWithWeapon.value, rangedData.attackDamage, weaponItemData);


            }
            Weapon_BulletManager.singleton.bulletsToTick.Add(arrowComponent);
        }

        void CreateGunShotNotification()
        {
            AIManager.singleton.SendNotificationToAi(rangedData.gunshotSoundNotificationDistance, transform.position);
            /*
            Debug.Log("Gun SHot");

            Collider[] collisionsInSphereCast = Physics.OverlapSphere(playerWeaponController.bulletShootPoint.position, rangedData.gunshotSoundNotificationDistance, playerInventoryController.enemyNotificationsMask.value);
            foreach (Collider item in collisionsInSphereCast)
            {
                Debug.Log("Hit");
                item.GetComponent<AINotificationHandler>().searchAlertController.SearchAreaNotifcationRecieved(playerInventoryController.transform.position);
            }*/
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