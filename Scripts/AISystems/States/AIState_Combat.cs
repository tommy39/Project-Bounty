using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.FieldOfView;
using IND.Core.Characters.Health;
using IND.Core.AISystems.Inventory;
using IND.Core.AISystems.Movement;
using IND.Core.Characters.Animations;
using IND.Core.Weapons;

namespace IND.Core.AISystems.States
{
    [System.Serializable]
    public class AIState_Combat
    {
        private AIStateController stateController;
        private FieldOfViewController fieldOfViewController;
        [HideInInspector]public HealthControllerPlayer target;
        private AIInventoryController inventoryController;
        private AIMovementController movementController;
        private AnimationController animController;
        private WeaponItemRuntime runtimeWeaponInHand;
        #region Rotate To Target Variables

        private ScriptableFloat deltaTime;
        private Vector3 targetPoint;
        private Quaternion targetRotation;
        #endregion

        #region Line Of Sight Variables
        RaycastHit lineOfSightRay;
        Vector3 lineOfSightDir;
        Vector3 lineOfSightDesiredRayCastPosition;
        float lineOfSightRayDistance;
        private ScriptableLayerMask lineOfSightObstaclesLayerMask;
        #endregion

        float currentDistanceFromTarget;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
            fieldOfViewController = stateCon.GetComponentInChildren<FieldOfViewController>();
            inventoryController = stateCon.GetComponent<AIInventoryController>();
            movementController = stateCon.GetComponent<AIMovementController>();
            animController = stateCon.GetComponent<AnimationController>();
            lineOfSightObstaclesLayerMask = Resources.Load("LayerMasks/LayerMask_AI_Line Of Sight Obstacles") as ScriptableLayerMask;
            deltaTime = GetResource.DeltaTimer();
            runtimeWeaponInHand = inventoryController.currentWeaponRuntime;
            inventoryController.currentWeaponRuntime.AttackCooldownEnd();
            inventoryController.rangedWeaponRuntime.currentAmmoInMagazine = inventoryController.rangedWeaponRuntime.rangedData.maxMagazineAmmoAmount;
        }
        public void Tick()
        {
            if (!CheckIfTargetIsAlive())
            {
                stateController.ChangeState(stateController.startState);
                return;
            }

            RotateTowardsTarget();
            MoveTowardsTarget();

            if (CheckIfTargetIsInLineOfSight() == false)
            {
                UpdateAnimValues(1, 0);
                return;
            }

            if (CheckIsInCombatDistance() == false)
            {
                return;
            }

            EnterShootStance();

           if(CheckIfCanAttack() == false)
            {
                return;
            }

            AttackBasedOnWeaponType();
        }
        public void OnStateEnter()
        {
            target = Object.FindObjectOfType<HealthControllerPlayer>();

            if (target == null)
                return;

            //Snap To Target
            Vector3 lookat = (new Vector3(target.transform.position.x, stateController.transform.position.y, target.transform.position.z) - stateController.transform.position).normalized;
            stateController.transform.rotation = Quaternion.LookRotation(lookat);
        }
        public void OnStateExit()
        {

        }
        bool CheckIfTargetIsAlive()
        {
            if (target.isDead == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void RotateTowardsTarget()
        {
            targetPoint = new Vector3(target.transform.position.x, stateController.transform.position.y, target.transform.position.z) - stateController.transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            stateController.transform.rotation = Quaternion.Slerp(stateController.transform.rotation, targetRotation, deltaTime.value * inventoryController.rotateToAttackTargetSpeed);
        }
        void MoveTowardsTarget()
        {
            movementController.SetAgentDestination(target.transform.position);
        }
        bool CheckIfTargetIsInLineOfSight()
        {
            lineOfSightDesiredRayCastPosition = fieldOfViewController.castPoint.position;
            lineOfSightRayDistance = Vector3.Distance(stateController.transform.position, target.transform.position);
            lineOfSightDir = (target.transform.position - stateController.transform.position);
            Debug.DrawRay(stateController.transform.position + lineOfSightDesiredRayCastPosition, lineOfSightDir * 100f);
            if (!Physics.Raycast(lineOfSightDesiredRayCastPosition, lineOfSightDir, out lineOfSightRay, lineOfSightRayDistance + 1f, lineOfSightObstaclesLayerMask.value))
            {
                // Debug.Log("Is In Line Of Sight");
                return true;
            }
            else
            {
                return false;
            }
        }
        void UpdateAnimValues(float verticalValue, float horizontalValue)
        {
            animController.SetAnimFloat(animController.animData.verticalFloat, verticalValue);
            animController.SetAnimFloat(animController.animData.horizontalFloat, horizontalValue);
        }

        bool CheckIsInCombatDistance()
        {
            currentDistanceFromTarget = Vector3.Distance(target.transform.position, stateController.transform.position);
            if (currentDistanceFromTarget < inventoryController.currentInHandWeaponData.aiData.attackRangedStoppingDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void EnterShootStance()
        {
            UpdateAnimValues(0, 0);
            movementController.StopAgent();
        }

        bool CheckIfCanAttack()
        {
            if (runtimeWeaponInHand.canAttack == false)
                return false;

            switch (runtimeWeaponInHand.weaponItemData.weaponType)
            {
                case WeaponType.RANGED:
                    if(CheckIfRangedWeaponHasAmmo() == false)
                    {
                        inventoryController.rangedWeaponRuntime.currentAmmoInMagazine = inventoryController.rangedWeaponRuntime.rangedData.maxMagazineAmmoAmount;
                        return false;
                    }

                    break;
                case WeaponType.MELEE:
                    break;
                case WeaponType.THROWABLE:
                    break;
                case WeaponType.FISTS:
                    break;
            }

            return true;
        }

        bool CheckIfRangedWeaponHasAmmo()
        {
            if(inventoryController.rangedWeaponRuntime.currentAmmoInMagazine == 0)
            {
                return false;
            }
            else
            {
                return true;
            }         

        }

        private void AttackBasedOnWeaponType()
        {
            switch (runtimeWeaponInHand.weaponItemData.weaponType)
            {
                case WeaponType.RANGED:
                    RangedWeaponAttack();
                    break;
                case WeaponType.MELEE:
                    ActivateMeleeAttack();
                    break;
                case WeaponType.THROWABLE:
                    break;
                case WeaponType.FISTS:
                    break;
            }
        }

        #region RANGED ATTACKS
        private void RangedWeaponAttack()
        {
            runtimeWeaponInHand.canAttack = false;
            animController.SetAnimBool(animController.animData.isFiringBool, true);
            inventoryController.rangedWeaponRuntime.FireGunShared();
            stateController.StartCoroutine(RangedAttackCooldownCoroutine());
        }
        public IEnumerator RangedAttackCooldownCoroutine()
        {
            yield return new WaitForSeconds(runtimeWeaponInHand.weaponItemData.aiData.attackCooldown);
            RangedAttackCooldownEnd();
        }

        public void RangedAttackCooldownEnd()
        {
            if (runtimeWeaponInHand == null)
                return;

            runtimeWeaponInHand.canAttack = true;
            animController.SetAnimBool(animController.animData.isFiringBool, false);
        }

        #endregion

        #region Melee Attacks
        void ActivateMeleeAttack()
        {
            inventoryController.currentWeaponRuntime.canAttack = false;
            animController.SetAnimBool(animController.animData.isMeleeAttackingBool, true);
            animController.PlayAnimationHash(animController.animData.meleeAttackAnimationClass);
        }

        #endregion
    }
}