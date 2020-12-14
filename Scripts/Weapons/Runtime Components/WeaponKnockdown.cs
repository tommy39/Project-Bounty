using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player;
using IND.Core.AISystems;
using IND.Core.Characters.Health;
using IND.Core.Characters.Hitboxes;

namespace IND.Core.Weapons
{
    [RequireComponent(typeof(BoxCollider))]
    public class WeaponKnockdown : IND_Mono
    {
        [Required]public Collider collisionCollider;
       [Required] public WeaponItemRuntime weaponRuntime;

        [Button]
        private void Setup()
        {
            collisionCollider = GetComponent<Collider>();
            weaponRuntime = GetComponentInParent<WeaponItemRuntime>();
            collisionCollider.isTrigger = true;
        }



        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponentInParent<HealthControllerAI>() != null)
            {
                HealthControllerAI healthController = other.gameObject.GetComponentInParent<HealthControllerAI>();
                if(weaponRuntime.weaponItemData.isLethalWhenThrown == true) //Apply Kill when true
                {
                    healthController.TakeDamage(1000, HitboxType.NULL);
                }
                else
                {
                    healthController.knockdownController.ApplyKnockdown();
                }   
            }
        }

        private void Reset()
        {
            Setup();
        }
    }
}