using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using IND.Core.UI.Challenges;
using IND.Core.Missions;
using IND.Core.UI.Cursors.AimCursor;

namespace IND.Core.UI.PauseMenu
{
    public class PauseMenuController : IND_Mono
    {
        [SerializeField] private GameObject pauseWindow;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitToMenuButton;
        [SerializeField] private Button quitToGameButton;

        private AimCursorManager aimCursorManager;
        private UI_ChallengesController challengesController;

        private bool isOpen = false;
        private bool hasBeenOpenedBefore = false;
        private ScriptableInputBoolAction pauseMenuInput;

        public override void Init()
        {
            pauseMenuInput = Resources.Load("Input Actions/Input Action Pause Menu") as ScriptableInputBoolAction;
            challengesController = GetComponent<UI_ChallengesController>();
            challengesController.Init();
            aimCursorManager = AimCursorManager.singleton;

            resumeButton.onClick.AddListener(CloseMenu);
            quitToGameButton.onClick.AddListener(Application.Quit);

            if (pauseWindow.activeSelf == true)
            {
                CloseMenu();
            }
        }

        public override void Tick()
        {
            if (pauseMenuInput.value.value)
            {
                if (isOpen == false)
                {
                    OpenMenu();
                }
                else
                {
                    CloseMenu();
                }
            }
        }

        void CloseMenu()
        {
            isOpen = false;
            pauseWindow.SetActive(false);
            aimCursorManager.DisableMouseCursor(false);
        }

        void OpenMenu()
        {
            if (hasBeenOpenedBefore == false)
            {
                FirstTimeOpen();
            }

            isOpen = true;
            pauseWindow.SetActive(true);
            challengesController.UpdateChallenges();
            aimCursorManager.EnableMouseCursor(false);
        }

        void FirstTimeOpen()
        {
            hasBeenOpenedBefore = true;
            challengesController.FirstTimeSetup();
        }
    }
}