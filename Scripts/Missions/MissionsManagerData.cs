using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Missions
{
    [CreateAssetMenu(fileName = "Missions Manager Data", menuName = "IND/Core/Missions/Missions Manager Data")]
    public class MissionsManagerData : ScriptableObject
    {
        [InlineEditor] public List<MissionData> missions = new List<MissionData>();
    }
}