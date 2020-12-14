using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.UI.MainMenu.Campaign;

namespace IND.Core.UI.MainMenu
{
    public class MainMenuManager : IND_Mono
    {
        private MainMenuBaseController mainMenuBaseController;
        private CampaignMenuManager campaignMenuManager;
        public override void Init()
        {
            mainMenuBaseController = GetComponentInChildren<MainMenuBaseController>();
            campaignMenuManager = GetComponentInChildren<CampaignMenuManager>();

            mainMenuBaseController.Init();
            campaignMenuManager.Init();
        }

        public override void Tick()
        {
            mainMenuBaseController.Tick();
            campaignMenuManager.Tick();
        }
    }
}