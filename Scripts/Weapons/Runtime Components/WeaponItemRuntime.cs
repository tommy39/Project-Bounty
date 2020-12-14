using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Weapons;
using IND.Core.AISystems.Inventory;
using IND.Core.Player.Inventory;
using IND.Core.Characters.Animations;
using IND.Core.Player.Combat;

namespace IND.Core.Weapons
{
    public class WeaponItemRuntime : IND_Mono
    {
        [HideInInspector] public PlayerInventoryController playerInventoryController;
        [HideInInspector] public PlayerWeaponController playerWeaponController;
        [HideInInspector] public AIInventoryController aiInventoryController;

        [InlineEditor] public WeaponItem weaponItemData;
        public bool isPlayerWeapon;

        [HideInInspector] public AnimationController animController;
        [HideInInspector] public bool canAttack = true;

        public List<BoxCollider> interactionCollisionColliders = new List<BoxCollider>();

        public List<WeaponKnockdown> knockdownWeaponColliders = new List<WeaponKnockdown>();

        public override void Init()
        {            
            DisableKnockdownCollidersInstant();
        }
        public virtual void PickupWeaponPlayer() //Called when spawned with or picked up from the ground
        {
            animController = GetComponentInParent<AnimationController>();
            DisableCollisionColliders();
            playerInventoryController = GetComponentInParent<PlayerInventoryController>();
            if (playerInventoryController == null)
            {
                aiInventoryController = GetComponentInParent<AIInventoryController>();
                isPlayerWeapon = false;
            }
            else
            {
                playerWeaponController = playerInventoryController.weaponController;
                isPlayerWeapon = true;
            }

            for (int i = 0; i < knockdownWeaponColliders.Count; i++)
            {
                knockdownWeaponColliders[i].collisionCollider.enabled = false;
            }
        }

        public virtual void FreshSpawnWeapon()
        {
            WeaponKnockdown[] knockdownComponents = GetComponentsInChildren<WeaponKnockdown>();
            foreach (WeaponKnockdown item in knockdownComponents)
            {
                knockdownWeaponColliders.Add(item);
            }
        }

        [Button]
        private void GetInteractionCollisionColliders()
        {
            interactionCollisionColliders.Clear();
            WeaponCollisionIdentifier[] identifiers = GetComponentsInChildren<WeaponCollisionIdentifier>();
            foreach (WeaponCollisionIdentifier item in identifiers)
            {
                interactionCollisionColliders.Add(item.GetComponent<BoxCollider>());
            }
        }
        public void EnableCollisionColliders()
        {
            for (int i = 0; i < interactionCollisionColliders.Count; i++)
            {
                interactionCollisionColliders[i].enabled = true;
            }
        }
        public void DisableCollisionColliders()
        {
            for (int i = 0; i < interactionCollisionColliders.Count; i++)
            {
                interactionCollisionColliders[i].enabled = false;
            }
        }

        public virtual void WeaponAttack()
        {
            canAttack = false;
            animController.SetAnimBool(animController.animData.isFiringBool, true);
            StartCoroutine("AttackCooldownCoroutine");
        }
        public virtual IEnumerator AttackCooldownCoroutine()
        {
            yield return new WaitForSeconds(weaponItemData.attackRate);
            AttackCooldownEnd();
        }

        public virtual void AttackCooldownEnd()
        {
            canAttack = true;
            animController.SetAnimBool(animController.animData.isFiringBool, false);
            StopCoroutine("AttackCooldownCoroutine");
        }


        public virtual void OnDropWeaponToGround()
        {
            for (int i = 0; i < knockdownWeaponColliders.Count; i++)
            {
                knockdownWeaponColliders[i].collisionCollider.enabled = true;
            }

            StartCoroutine("DisableKnockdownColliders");
        }

        private IEnumerator DisableKnockdownColliders()
        {
            yield return new WaitForSeconds(weaponItemData.knockdownCollidersDisableTimer);
            for (int i = 0; i < knockdownWeaponColliders.Count; i++)
            {
                knockdownWeaponColliders[i].collisionCollider.enabled = false;
            }
        }

        [Button]
        public void GetKnockdownColliders()
        {
            knockdownWeaponColliders.Clear();

            WeaponKnockdown[] knockdownComponents = GetComponentsInChildren<WeaponKnockdown>();
            foreach (WeaponKnockdown item in knockdownComponents)
            {
                knockdownWeaponColliders.Add(item);
            }
        }
        void DisableKnockdownCollidersInstant()
        {
            for (int i = 0; i < knockdownWeaponColliders.Count; i++)
            {
                knockdownWeaponColliders[i].collisionCollider.enabled = false;
            }
        }
        private void Reset()
        {
            GetInteractionCollisionColliders();
            GetKnockdownColliders();
        }
    }
}