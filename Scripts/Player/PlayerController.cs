using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Movement;
using IND.Core.Player.Actions;
using IND.Core.Player.Rotation;
using IND.Core.Characters.Animations;
using IND.Core.Player.Inventory;
using IND.Core.Player.Combat;
using IND.Core.Characters.Health;
using IND.Core.WorldInteractions;
using IND.Core.Player.Weapons;
using IND.Core.Characters.LimbGibbings;
using IND.Core.Characters.Hitboxes;
using IND.Core.Player.Executions;
using IND.Core.Player.Abilities;

namespace IND.Core.Player
{
    public class PlayerController : IND_Mono
    {
        [Required] public Transform cameraFocusPoint;
        [Required] [InlineEditor] public ScriptableBool tickPlayer;

        private PlayerMovementController movementController;
        private PlayerActionController actionController;
        private PlayerRotationController rotationController;
        private PlayerAnimationController animationController;
        [HideInInspector] public PlayerInventoryController inventoryController;
        private PlayerWeaponController weaponController;
        private HealthController healthController;
        private PlayerReloadController reloadController;
        private InteractionSearcherController interactionController;
        private LimbsController limbsController;
        [HideInInspector] public HitboxController hitboxController;
        private PlayerExecutionController executionController;
        private AbilityController abilityController;

        public override void Init()
        {
            movementController = GetComponent<PlayerMovementController>();
            actionController = GetComponent<PlayerActionController>();
            rotationController = GetComponent<PlayerRotationController>();
            animationController = GetComponent<PlayerAnimationController>();
            inventoryController = GetComponent<PlayerInventoryController>();
            weaponController = GetComponent<PlayerWeaponController>();
            healthController = GetComponent<HealthController>();
            reloadController = GetComponent<PlayerReloadController>();
            interactionController = GetComponent<InteractionSearcherController>();
            executionController = GetComponent<PlayerExecutionController>();
            limbsController = GetComponentInChildren<LimbsController>();
            hitboxController = GetComponentInChildren<HitboxController>();
            abilityController = GetComponent<AbilityController>();

            tickPlayer.value = true;

            movementController.Init();
            actionController.Init();
            rotationController.Init();
            animationController.Init();
            weaponController.Init();
            inventoryController.Init();
            healthController.Init();
            reloadController.Init();
            interactionController.Init();
            executionController.Init();
            limbsController.Init();
            hitboxController.Init();
            abilityController.Init();

        }

        public override void Tick()
        {
            if (tickPlayer.value == false)
                return;

            movementController.Tick();
            actionController.Tick();
            rotationController.Tick();
            animationController.Tick();
            inventoryController.Tick();
            weaponController.Tick();
            healthController.Tick();
            reloadController.Tick();
            interactionController.Tick();
            executionController.Tick();
            limbsController.Tick();
            abilityController.Tick();
        }

        public override void FixedTick()
        {
            if (tickPlayer.value == false)
                return;

            movementController.FixedTick();
            actionController.FixedTick();
            rotationController.FixedTick();
            animationController.FixedTick();
            inventoryController.FixedTick();
            weaponController.FixedTick();
            abilityController.FixedTick();
        }
    }
}