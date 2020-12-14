using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Health;
using IND.Core.Characters.Hitboxes;
using IND.Core.HitEffects;

namespace IND.Core.Weapons
{
    public class Weapon_FiredProjectile_Arrow : Weapon_FiredProjectile
    {
        public override void Init(float targetSpeed, LayerMask targetMask, int damageToDeal, WeaponItem weapon)
        {
            base.Init(targetSpeed, targetMask, damageToDeal, weapon);
        }


        public override void FixedTick()
        {
            base.FixedTick();
        }

        public override void HitObject()
        {
            healthControllerHit = hit.transform.GetComponentInParent<HealthController>();
            if (healthControllerHit != null) //Hit a Player Or AI, WIll not execute any other logic outside of here 
            {
                hittableObjectController = healthControllerHit.GetComponent<HittableObjectController>();
                hittableObjectController.ObjectHit(hit.point, originalFiredPosition);
                HitboxColliderHandler colliderHandler = hit.transform.GetComponent<HitboxColliderHandler>();
                colliderHandler.parentHandler.parentRigidbody.AddForce(colliderHandler.transform.forward * 1000f, ForceMode.Impulse);
                if (Physics.Raycast(rayCastPoint.position, transform.TransformDirection(Vector3.forward), out hit, 1.3f, layerMask)) //Cast a second ray to get the body part to attach too
                {
                    transform.position = hit.point + (transform.forward * weaponItem.arrowBodyHitOffset);
                    transform.SetParent(hit.transform);
                }            

                SubHitboxController hitboxController = hit.collider.GetComponent<SubHitboxController>();
                healthControllerHit.TakeDamage(damage, hitboxController.hitboxType);

                DestroyProjectile();


                return;
            }

            hittableObjectController = hit.transform.GetComponent<HittableObjectController>();
            if (hittableObjectController != null)
            {
                // Debug.Log(3);
                hittableObjectController.ObjectHit(hit.point, -hit.normal);
                transform.position = hit.point;
            }
            DestroyProjectile();
        }

        public override void DestroyProjectile()
        {
            // Vector3 hitWallPoint = hit.point;
            Weapon_BulletManager.singleton.bulletsToTick.Remove(this);
           // PoolingManager.singleton.bulletPooler.ReIntergratePoolableObject(gameObject);
           // transform.position = hit.point;
        }
    }
}