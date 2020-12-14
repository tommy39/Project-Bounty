using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Missions;
using IND.Core.Client;

namespace IND.Core.Challenges
{
    public class ChallengeManager : IND_Mono
    {
        public List<ChallengeRuntime> activeChalleneges = new List<ChallengeRuntime>();
        public List<ChallengeData> completedChallenges = new List<ChallengeData>();

        [SerializeField] [InlineEditor] private ScriptableBool forceMissionToUncompletedOnStart;
        private MissionData missionData;

        public override void Init()
        {   
            missionData = GetMissionData();

            SetupActiveMissions();
        }

        public override void Tick()
        {
            for (int i = 0; i < activeChalleneges.Count; i++)
            {
                activeChalleneges[i].Tick();
            }
        }

        private MissionData GetMissionData()
        {
            return FindObjectOfType<MissionController>().missionData;
        }

        private void SetupActiveMissions()
        {
            for (int i = 0; i < missionData.challenges.Count; i++)
            {
                missionData.challenges[i].ChallengeInit(this);
            }
        }
    }

}