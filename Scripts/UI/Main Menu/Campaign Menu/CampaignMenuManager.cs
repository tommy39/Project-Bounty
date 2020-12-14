using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using IND.Core.Statics.Locations;

namespace IND.Core.UI.MainMenu.Campaign
{
    public class CampaignMenuManager : IND_Mono
    {
        private SelectedMissionController selectedMissionController;
        private AvailableMissionsController availableMissionsController;

        [SerializeField] private GameObject parentGroup;
        [SerializeField] private Button playButton;
       
        public override void Init()
        {
            selectedMissionController = GetComponentInChildren<SelectedMissionController>();
            availableMissionsController = GetComponentInChildren<AvailableMissionsController>();

            availableMissionsController.Init();
            selectedMissionController.Init();
            playButton.onClick.AddListener(PlaySelectedMission);
            CloseMenu();
        }
        public void OpenMenu()
        {
            parentGroup.SetActive(true);
        }

        public void CloseMenu()
        {
            parentGroup.SetActive(false);
        }
      
        private void PlaySelectedMission()
        {
            LoadLocation.Execute(FindObjectOfType<SelectedMissionController>().selectedMission.locationData);
        }
    }
}