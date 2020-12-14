using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.Characters.Health;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;
using IND.Core.HitEffects;
using IND.Core.Pooling;
using IND.Core.Weapons;

namespace IND.Core.Weapons
{
    public class Weapon_FiredProjectile : MonoBehaviour
    {
        [HideInInspector] public LayerMask layerMask;
        [HideInInspector] public float speed;
        [HideInInspector] public int damage;
        [HideInInspector] public WeaponItemRanged weaponItem;
        [Required] public ScriptableFloat fixedDeltaTime;
        [Required] public Transform rayCastPoint;

       [HideInInspector] public HealthController healthControllerHit;
        [HideInInspector] public HittableObjectController hittableObjectController;
        [HideInInspector] public RaycastHit hit;
        [HideInInspector] public Vector3 originalFiredPosition;
        public virtual void Init(float targetSpeed, LayerMask targetMask, int damageToDeal, WeaponItem weapon)
        {
            layerMask = targetMask;
            speed = targetSpeed;
            damage = damageToDeal;
            weaponItem = weapon as WeaponItemRanged;
            originalFiredPosition = transform.position;
        }

        public virtual void FixedTick()
        {
            transform.position += transform.forward * speed * fixedDeltaTime.value;

            if (Physics.Raycast(rayCastPoint.position, transform.TransformDirection(Vector3.forward), out hit, 1.3f, layerMask))
            {
                HitObject();
            }
        }

      public virtual void HitObject()
        {
            healthControllerHit = hit.transform.GetComponentInParent<HealthController>();
            HealthControllerProp propHealthController = healthControllerHit as HealthControllerProp;
            if (healthControllerHit != null && propHealthController == null) //Hit a Player Or AI, WIll not execute any other logic outside of here 
            {
                hittableObjectController = hit.collider.GetComponent<HittableObjectController>();
                hittableObjectController.ObjectHit(hit.point, originalFiredPosition);
                SubHitboxController hitboxController = hit.collider.GetComponent<SubHitboxController>();
                healthControllerHit.TakeDamage(damage, hitboxController.hitboxType);     
                DestroyProjectile();
                return;
            }
            if(propHealthController != null)
            {
                healthControllerHit.TakeDamage(damage, HitboxType.PROP);
                DestroyProjectile();
            }

            hittableObjectController = hit.transform.GetComponent<HittableObjectController>();
            if (hittableObjectController != null)
            {
                // Debug.Log(3);
                hittableObjectController.ObjectHit(hit.point, -hit.normal);
            }
            DestroyProjectile();
        }

        public virtual void DestroyProjectile()
        {
            Weapon_BulletManager.singleton.bulletsToTick.Remove(this);
            PoolingManager.singleton.bulletPooler.ReIntergratePoolableObject(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawRay(rayCastPoint.position, transform.forward * 2f);
        }

    }
}