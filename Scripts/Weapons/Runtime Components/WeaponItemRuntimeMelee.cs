using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    public class WeaponItemRuntimeMelee : WeaponItemRuntime
    {
        public List<WeaponMeleeAttackCollider> attackColliders = new List<WeaponMeleeAttackCollider>();

        public override void Init()
        {
            base.Init();
            canAttack = true;
            for (int i = 0; i < attackColliders.Count; i++)
            {
                if (playerInventoryController != null)
                {
                    attackColliders[i].SetLayerMask(playerInventoryController.playerMeleeWeaponLayerMask);
                }
                else
                {
                    attackColliders[i].SetLayerMask(aiInventoryController.meleeWeaponEnemyLayerMask);
                }
            }
        }

        [Button]
        public void GetAttackColliders()
        {
            WeaponMeleeAttackCollider[] attackCollisionRays = GetComponentsInChildren<WeaponMeleeAttackCollider>();
            foreach (WeaponMeleeAttackCollider item in attackCollisionRays)
            {
                attackColliders.Add(item);
                item.DisableCollider();
            }
        }

        public override void PickupWeaponPlayer()
        {
            base.PickupWeaponPlayer();

          
        }

        public override void FreshSpawnWeapon()
        {
            base.FreshSpawnWeapon();

        }

        public void StartMeleeAttack()
        {
            canAttack = false;
            animController.SetAnimBool(animController.animData.isMeleeAttackingBool, true);
        }

        public void EnableAttackColliders()
        {
            for (int i = 0; i < attackColliders.Count; i++)
            {
                attackColliders[i].EnableCollider();
            }
        }

        public void DisableAttackColliders()
        {
            for (int i = 0; i < attackColliders.Count; i++)
            {
                attackColliders[i].DisableCollider();
            }
        }

     
    }
}