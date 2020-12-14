using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Missions;

namespace IND.Core.UI.MainMenu.Campaign
{
    public class AvailableMissionsController : IND_Mono
    {
        [SerializeField] private GameObject selectableMissionPrefab;
        [SerializeField] private Transform missionsGroup;
        [InlineEditor] public MissionsManagerData missionsManagerData;
        
        public override void Init()
        {
            //Check For Existing Template
            SelectableMissionController[] existingSelectableMissions = GetComponentsInChildren<SelectableMissionController>();
            foreach (SelectableMissionController item in existingSelectableMissions)
            {
                Destroy(item.gameObject);
            }

            for (int i = 0; i < missionsManagerData.missions.Count; i++)
            {
                CreateSelectableMission(missionsManagerData.missions[i]);
            }
        }

        void CreateSelectableMission(MissionData mission)
        {
            GameObject geo = Instantiate(selectableMissionPrefab, missionsGroup);
            SelectableMissionController controller = geo.GetComponent<SelectableMissionController>();
            controller.SetupSelecteableMission(mission);
        }
    }
}