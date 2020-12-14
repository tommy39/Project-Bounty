using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Missions;
using TMPro;
using UnityEngine.UI;
using IND.Core.Challenges;

namespace IND.Core.UI.MainMenu.Campaign
{
    public class SelectedMissionController : IND_Mono
    {
        [SerializeField] private GameObject challengePrefab;
        [HideInInspector] public MissionData selectedMission;
        [SerializeField] private TextMeshProUGUI missionName;
        [SerializeField] private TextMeshProUGUI longDescription;
        [SerializeField] private Image icon;
        [SerializeField] private Transform challengesParent;

        public override void Init()
        {
            //Delete Any Existing Challeneges that there might be
            CreatedChallengeUIController[] existingChallenges = GetComponentsInChildren<CreatedChallengeUIController>();

            foreach (CreatedChallengeUIController item in existingChallenges)
            {
                Destroy(item.gameObject);
            }

            //Select First Mission On Start
            SetupSelectedMission(FindObjectOfType<AvailableMissionsController>().missionsManagerData.missions[0]);
        }

        public void SetupSelectedMission(MissionData missionData)
        {
            selectedMission = missionData;
            missionName.text = missionData.missionName;
            longDescription.text = missionData.longDescription;
            icon.sprite = missionData.icon;
            for (int i = 0; i < missionData.challenges.Count; i++)
            {
                CreateChallenges(missionData.challenges[i]);
            }
        }

        private void CreateChallenges(ChallengeData challenegeData)
        {
            GameObject geo = Instantiate(challengePrefab, challengesParent);
            CreatedChallengeUIController challenegeController = geo.GetComponent<CreatedChallengeUIController>();
            challenegeController.SetupChallenge(challenegeData);
        }
    }
}