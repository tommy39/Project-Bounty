using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Characters.Animations;
using IND.Core.WorldInteractions;
using IND.Core.Characters;

namespace IND.Core.AISystems.Inventory
{
    public class AIInventoryController : IND_Mono
    {
        [HideInInspector] public AIController aiController;
        private AnimationController animController;

        [HideInInspector] public WeaponItem currentInHandWeaponData;
        [ShowIf("currentWeaponRuntime")] public WeaponItemRuntime currentWeaponRuntime;

        [HideInInspector] public WeaponItemRuntimeRanged rangedWeaponRuntime;
        [HideInInspector] public WeaponItemRuntimeMelee meleeWeaponRuntime;

        private Transform rightHandTrans;
        [Required] public Transform bulletShootPoint;
        private Transform worldInteractionParentGroup;

        public float rotateToAttackTargetSpeed = 20f;

        [InlineEditor] [Required] public ScriptableLayerMask meleeWeaponEnemyLayerMask;
        public override void Init()
        {
            aiController = GetComponent<AIController>();
            worldInteractionParentGroup = WorldInteractionsManager.singleton.transform;
            animController = GetComponent<AnimationController>();
            rightHandTrans = GetComponentInChildren<RightHandIdentifier>().transform;
            currentWeaponRuntime = GetComponentInChildren<WeaponItemRuntime>();
            currentInHandWeaponData = currentWeaponRuntime.weaponItemData;
            currentWeaponRuntime.animController = animController;

            if (currentInHandWeaponData != null)
            {
                EquipWeaponOnGameStart();
            }
        }

         void EquipWeaponOnGameStart()
        {         
            currentWeaponRuntime.aiInventoryController = this;
            currentWeaponRuntime.FreshSpawnWeapon();
            currentWeaponRuntime.Init();            
            currentWeaponRuntime.DisableCollisionColliders();

            animController.SetAnimInt(animController.animData.weaponAnimID, currentWeaponRuntime.weaponItemData.weaponAnimID);
            animController.PlayAnimationHash(animController.animData.getNewWeaponAnimationClass);

            switch (currentInHandWeaponData.weaponType)
            {
                case WeaponType.RANGED:
                    EquipRangedWeapon();
                    break;
                case WeaponType.MELEE:
                    EquipMeleeWeapon();
                    break;
            }
        }

        public void EquipExistingWeapon(WeaponItemRuntime weapon)
        {
            weapon.transform.SetParent(rightHandTrans);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            currentWeaponRuntime = weapon;
            currentInHandWeaponData = weapon.weaponItemData;
            weapon.PickupWeaponPlayer();
            weapon.DisableCollisionColliders();

            animController.SetAnimInt(animController.animData.weaponAnimID, currentWeaponRuntime.weaponItemData.weaponAnimID);
            animController.PlayAnimationHash(animController.animData.getNewWeaponAnimationClass);

            switch (currentInHandWeaponData.weaponType)
            {
                case WeaponType.RANGED:
                    EquipRangedWeapon();
                    break;
                case WeaponType.MELEE:
                    EquipMeleeWeapon();
                    break;
            }
        }


        void EquipRangedWeapon()
        {
            //  aimIkComponent.solver.clampWeight = currentInHandItemData.clampIKAimWeight;
            rangedWeaponRuntime = currentWeaponRuntime as WeaponItemRuntimeRanged;
        }

        void EquipMeleeWeapon()
        {
            meleeWeaponRuntime = currentWeaponRuntime as WeaponItemRuntimeMelee;

        }

        public override void Tick()
        {
            if (currentWeaponRuntime != null)
            {
                currentWeaponRuntime.Tick();
            }
        }


        public void DropCurrentInHandWeapon(bool dropWithForce)
        {
            if (currentWeaponRuntime == null) //Has No Weapon to Drop
                return;

            WorldInteractionEvent_PickupWeaponRuntime pickupWeaponInteraction = WeaponStaticUtils.DropWeaponToGround(currentWeaponRuntime, worldInteractionParentGroup, aiController.aiData.worldInteractionPrefab);
            if (dropWithForce)
            {
                WeaponStaticUtils.ThrowWeaponWithForce(pickupWeaponInteraction, currentInHandWeaponData.throwDistanceForce, transform.forward);
            }

            currentWeaponRuntime = null;
        }

    }
}