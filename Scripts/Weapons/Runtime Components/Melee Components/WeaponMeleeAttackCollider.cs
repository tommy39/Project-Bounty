using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Health;
using IND.Core.Shared.Statics;
using IND.Core.Characters.Hitboxes;

namespace IND.Core.Weapons
{
    [RequireComponent(typeof(BoxCollider))]
    public class WeaponMeleeAttackCollider : IND_Mono
    {
        [Required] public BoxCollider attackCollider;
        [Required] public WeaponItemRuntimeMelee meleeItem;

        public void SetLayerMask(ScriptableLayerMask Layer)
        {
            //      currentGameObject.layer = LayerMask.NameToLayer(layerName.Value);
           gameObject.layer = GetLayerFromLayerMask.GetInt(Layer.value);
        }

        [Button]
        public void GetCollider()
        {
            attackCollider = GetComponent<BoxCollider>();
            attackCollider.isTrigger = true;
            meleeItem = GetComponentInParent<WeaponItemRuntimeMelee>();
        }

        public void EnableCollider()
        {
            attackCollider.enabled = true;
        }

        public void DisableCollider()
        {
            attackCollider.enabled = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<HealthController>() != null)
            {
                SubHitboxController hitboxController = other.GetComponent<SubHitboxController>();
                //Debug.Log(hitboxController);
                other.GetComponentInParent<HealthController>().TakeDamage(meleeItem.weaponItemData.attackDamage, hitboxController.hitboxType);
            }
        }

        private void Reset()
        {
            GetCollider();
        }
    }
}