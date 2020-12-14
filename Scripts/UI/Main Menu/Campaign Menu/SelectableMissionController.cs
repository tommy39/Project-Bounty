using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Missions;
using UnityEngine.UI;
using TMPro;

namespace IND.Core.UI.MainMenu.Campaign
{
    public class SelectableMissionController : IND_Mono
    {
        public MissionData mission;
        [SerializeField] private TextMeshProUGUI textArea;
        [SerializeField] private Image icon;
        public void SetupSelecteableMission(MissionData missionData)
        {
            mission = missionData;
            textArea.text = missionData.missionName;
            icon.sprite = missionData.icon;
        }
    }
}