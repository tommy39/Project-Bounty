using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Health;
using IND.Core.Characters.Animations;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using IND.Core.AISystems.Movement;
using IND.Core.AISystems.Inventory;
using IND.Core.AISystems.States;
using IND.Core.Shared.Statics;
using IND.Core.AISystems.FieldOfView;
using IND.Core.AISystems.TroopSystems;

namespace IND.Core.AISystems
{
    public class AIController : IND_Mono
    {
        [Required] [InlineEditor] public AIData aiData;
        [HideInInspector] public AIManager aiManager;

        private LimbsController limbsController;
        [HideInInspector] public HitboxController hitboxController;
        [HideInInspector] public AITroopController aiTroopController;
        [HideInInspector] public AnimationController animController;
        [HideInInspector] public AIMovementController movementController;
        [HideInInspector] public AIInventoryController inventoryController;
        [HideInInspector] public AnimatorHookAI animHook;
        [HideInInspector] public HealthController healthController;
        [HideInInspector] public AISearchAlertController searchController;
        [HideInInspector] public FieldOfViewController fieldOfViewController;
        [HideInInspector] public AIAlertController aiAlertController;
        [HideInInspector] public AINotificationHandler notificationHandler;
        private AIStateController aiStateController;

        public override void Init()
        {
            aiManager = FindObjectOfType<AIManager>();
            limbsController = GetComponentInChildren<LimbsController>();

            animHook = GetComponentInChildren<AnimatorHookAI>();
            if (animHook == null)
            {
                animHook = GetComponentInChildren<Animator>().gameObject.AddComponent<AnimatorHookAI>();
            }

            hitboxController = GetComponentInChildren<HitboxController>();
            aiTroopController = GetComponent<AITroopController>();
            animController = GetComponent<AnimationController>();
            movementController = GetComponent<AIMovementController>();
            inventoryController = GetComponent<AIInventoryController>();
            healthController = GetComponent<HealthController>();
            searchController = GetComponent<AISearchAlertController>();
            aiAlertController = GetComponent<AIAlertController>();
            fieldOfViewController = GetComponentInChildren<FieldOfViewController>();
            notificationHandler = GetComponentInChildren<AINotificationHandler>();
            aiStateController = GetComponent<AIStateController>();

            UpdateTransformLayer(); //Assign Correct Layer to Game Object

            //Init Assigned Components
            limbsController.Init();
            hitboxController.Init();
            aiTroopController.Init();
            animController.Init();
            animHook.Init();
            movementController.Init();
            inventoryController.Init();
            healthController.Init();
            searchController.Init();
            aiAlertController.Init();
            fieldOfViewController.Init();
            aiStateController.Init();
            notificationHandler.Init();
        }

        public override void Tick()
        {
            limbsController.Tick();
            hitboxController.Tick();
            aiTroopController.Tick();
            animController.Tick();
            movementController.Tick();
            inventoryController.Tick();
            animHook.Tick();
            healthController.Tick();
            searchController.Tick();
            fieldOfViewController.Tick();
            aiAlertController.Tick();
            aiStateController.Tick();
            notificationHandler.Tick();
        }

        void UpdateTransformLayer()
        {
            ScriptableLayerMask enemyLayerMask = Resources.Load("LayerMasks/LayerMask_Enemy") as ScriptableLayerMask;
            int layerMaskNumber = GetLayerFromLayerMask.GetInt(enemyLayerMask.value);
            gameObject.layer = layerMaskNumber;
        }
    }
}