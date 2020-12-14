using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Challenges;
using IND.Core.Locations;


namespace IND.Core.Missions
{
    [CreateAssetMenu(fileName = "Mission Data", menuName = "IND/Core/Missions/Mission Data")]
    public class MissionData : ScriptableObject
    {
        public string missionName;
        public Location locationData;
        public Sprite icon;
        [TextArea(0, 24)] public string longDescription;

        [InlineEditor] public List<ChallengeData> challenges = new List<ChallengeData>();
    }
}