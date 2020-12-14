using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Health
{
    [CreateAssetMenu(fileName = "HealthData", menuName = "IND/Core/Character/Health/HealthData")]
    public class HealthData : ScriptableObject
    {
        public int maxHealth = 100;
    }
}