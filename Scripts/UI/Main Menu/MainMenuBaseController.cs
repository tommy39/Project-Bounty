using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using IND.Core.UI.MainMenu.Campaign;

namespace IND.Core.UI.MainMenu
{
    public class MainMenuBaseController : IND_Mono
    {
        private CampaignMenuManager campaignMenuManager;

        [SerializeField] private GameObject parentGroup;

        [SerializeField] private Button campaignButton;
        public override void Init()
        {
            campaignMenuManager = FindObjectOfType<CampaignMenuManager>();

            campaignButton.onClick.AddListener(OpenCampaignMenu);
            EnableMenu();
        }

        public override void Tick()
        {

        }

        private void OpenCampaignMenu()
        {
            campaignMenuManager.OpenMenu();
            DisableMenu();

        }

        public void DisableMenu()
        {
            parentGroup.SetActive(false);
        }

        public void EnableMenu()
        {
            parentGroup.SetActive(true);
        }
    }
}