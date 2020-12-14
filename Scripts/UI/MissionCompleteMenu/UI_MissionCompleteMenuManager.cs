using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.UI.MissionCompleteMenu
{
    public class UI_MissionCompleteMenuManager : IND_Mono
    {
        [SerializeField] protected GameObject textObj;
        private bool isActive = false;

        private void Start()
        {
            textObj.SetActive(false);
        }

        private void Update()
        {
            if(isActive == true)
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    LevelRestart.UI_LevelRestartManager.singleton.RestartLevel();
                }
            }
        }

        public void OnMissionComplete()
        {
            textObj.SetActive(true);
            isActive = true;
        }
    }
}