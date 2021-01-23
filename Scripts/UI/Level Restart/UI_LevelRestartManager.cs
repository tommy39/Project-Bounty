using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using IND.Core.Locations;
using IND.Core.UI.Cursors.AimCursor;
using IND.Core.Managers.Level;

namespace IND.Core.UI.LevelRestart
{
    public class UI_LevelRestartManager : IND_Mono
    {
        public GameObject restartWindow;
        public bool restartMenuIsActivate = false;

        public ScriptableBool restartInput;
        public bool allowRestartMenuToAppear = false;

        private Location locationData;
        private AimCursorManager aimCursor;

        public override void Init()
        {
            restartWindow.gameObject.SetActive(false);

            locationData = FindObjectOfType<LevelManager>().locationData;
            aimCursor = FindObjectOfType<AimCursorManager>();
        }

        public override void Tick()
        {
            if (restartMenuIsActivate == false)
                return;

            if(restartInput.value)
            {
                RestartLevel();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene("LevelType_Gameplay", LoadSceneMode.Single);
            SceneManager.LoadScene(locationData.locationName + "_Settings", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Gameplay", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Ground", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Props", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Buildings", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Lighting", LoadSceneMode.Additive);
            SceneManager.LoadScene("MasterScene", LoadSceneMode.Additive);
        }

        public void ActivateRestartMenu()
        {
            if (allowRestartMenuToAppear == true)
            {
                restartWindow.SetActive(true);
            }
            restartMenuIsActivate = true;
            aimCursor.aimTransform.gameObject.SetActive(false);
        }

        public static UI_LevelRestartManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}