using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Weapons;
using IND.Core.Player.Inventory;
using IND.Core.Characters.Animations;
using IND.Core.Characters.Health;
using IND.Core.Characters.Hitboxes;

namespace IND.Core.Weapons.Grenades
{
    public class GrenadeItemRuntime : IND_Mono
    {
        [InlineEditor] public GrenadeItem grenadeItemData;

        public Collider collisionCollider;
        public Rigidbody rigidBod;
        [Required] public GameObject grenadeMesh;

        [Button]
        public void Setup()
        {
            rigidBod = GetComponent<Rigidbody>();
            collisionCollider = GetComponent<Collider>();
            rigidBod.mass = grenadeItemData.rigidBodyData.mass;
            rigidBod.drag = grenadeItemData.rigidBodyData.drag;
            rigidBod.angularDrag = grenadeItemData.rigidBodyData.angularDrag;
        }


        public void BeginThrow()
        {
            rigidBod.AddForce(transform.forward * grenadeItemData.throwForce);

            StartCoroutine("StartTimer");

        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(grenadeItemData.timeBeforeExploding);
            Explode();
        }

        private void Explode()
        {
            Destroy(grenadeMesh);
            GameObject explosionGeo = Instantiate(grenadeItemData.explosionEffect);
            explosionGeo.transform.position = transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, grenadeItemData.explosionRadius, grenadeItemData.hitablesLayerMask.value);

            foreach (Collider item in hitColliders)
            {
                if(item.GetComponentInParent<HealthController>() != null)
                {
                    HealthController healthcon = item.GetComponentInParent<HealthController>();
                    healthcon.TakeDamage(grenadeItemData.damage, HitboxType.NULL);
                    if(healthcon.GetType() ==typeof(HealthControllerAI))
                    {
                        HealthControllerAI healthconAI = healthcon as HealthControllerAI;
                    }
                }
            }

            StartCoroutine("FinalDestroy");
        }

        private IEnumerator FinalDestroy()
        {
            yield return new WaitForSeconds(grenadeItemData.destroyFinalTimer);
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, grenadeItemData.explosionRadius);
        }
    }
}