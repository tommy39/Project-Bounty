using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Cameras;
using IND.Core.Player;
using IND.Core.Shared;
using IND.Core.Pooling;
using IND.Core.UI.Cursors;
using IND.Core.UI.LevelRestart;
using IND.Core.UI.Weapons;
using IND.Core.UI.WorldInteractions;
using IND.Core.Weapons;
using IND.Core.UI.Cursors.AimCursor;
using IND.Core.UI.Weapons.Reload;
using IND.Core.WorldInteractions;
using UnityEngine.SceneManagement;
using IND.Core.AISystems;
using IND.Core.Challenges;
using IND.Core.UI.PauseMenu;
using IND.Core.Missions;
using IND.Core.UI.WeaponSelectionMenu;

namespace IND.Core.Managers.Level
{
    public class LevelManagerGameplay : LevelManager
    {
        private CamControllerGameplay camController;
        private PlayerController playerController;
        private MoveToMousePosition moveToMousePos;
        private PoolingManager poolingManager;
        private Weapon_BulletManager bulletManager;
        private WorldInteractionsManager worldInteractionsManager;
        private ChallengeManager challenegeManager;

        private AimCursorManager aimCursorManager;
        private UI_WorldInteractionManager uiWorldInteractionManager;
        private UI_LevelRestartManager uiLevelRestartManager;
        private UI_ReloadProgressBar uiReloadProgressBar;
        private PauseMenuController pauseMenuController;
        private WeaponSelectionMenuManager weaponSelectionMenuManager;

        private AIManager aiManager;

        public override void Init()
        {
            LoadLevelType();

            camController = FindObjectOfType<CamControllerGameplay>();
            poolingManager = FindObjectOfType<PoolingManager>();
            playerController = FindObjectOfType<PlayerSpawner>().SpawnPlayer();
            moveToMousePos = FindObjectOfType<MoveToMousePosition>();
            bulletManager = FindObjectOfType<Weapon_BulletManager>();
            worldInteractionsManager = FindObjectOfType<WorldInteractionsManager>();
            challenegeManager = FindObjectOfType<ChallengeManager>();

            aimCursorManager = FindObjectOfType<AimCursorManager>();
            uiWorldInteractionManager = FindObjectOfType<UI_WorldInteractionManager>();
            uiLevelRestartManager = FindObjectOfType<UI_LevelRestartManager>();
            uiReloadProgressBar = FindObjectOfType<UI_ReloadProgressBar>();
            pauseMenuController = FindObjectOfType<PauseMenuController>();
            weaponSelectionMenuManager = FindObjectOfType<WeaponSelectionMenuManager>();

            aiManager = FindObjectOfType<AIManager>();

            camController.Init();
            poolingManager.Init();
            moveToMousePos.Init();
            bulletManager.Init();
            playerController.Init();
            worldInteractionsManager.Init();

            aimCursorManager.Init();
            uiWorldInteractionManager.Init();
            uiLevelRestartManager.Init();
            uiReloadProgressBar.Init();
            weaponSelectionMenuManager.Init();

            aiManager.Init();
            challenegeManager.Init();
            pauseMenuController.Init();


            //Place Code Above Here
            base.Init();
        }

        public override void Tick()
        {
            camController.Tick();
            poolingManager.Tick();
            moveToMousePos.Tick();
            bulletManager.Tick();
            playerController.Tick();
            worldInteractionsManager.Tick();
            challenegeManager.Tick();

            aimCursorManager.Tick();
            uiWorldInteractionManager.Tick();
            uiLevelRestartManager.Tick();
            uiReloadProgressBar.Tick();
            pauseMenuController.Tick();
            weaponSelectionMenuManager.Tick();

            aiManager.Tick();

            //Place Code Above Here
            base.Tick();
        }

        public override void FixedTick()
        {
            camController.FixedTick();
            bulletManager.FixedTick();
            playerController.FixedTick();
            worldInteractionsManager.FixedTick();

            //Place Code Above Here
            base.FixedTick();
        }

        public override void LateTick()
        {
            camController.LateTick();
            playerController.LateTick();

            //Place Code Above Here
            base.LateTick();
        }
    }
}