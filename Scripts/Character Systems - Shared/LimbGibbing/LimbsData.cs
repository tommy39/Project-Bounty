using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.LimbGibbings
{
    [CreateAssetMenu(fileName = "Character_LimbsData", menuName = "IND/Core/Character/LimbsGibbing/LimbsData")]
    public class LimbsData : ScriptableObject
    {
        public GameObject leftArmPrefab;
        public GameObject rightArmPrefab;
        public GameObject torsoPrefab;
        public GameObject leftLegPrefab;
        public GameObject rightLegPrefab;
        public GameObject headPrefab;

        [InlineEditor] public ScriptableLayerMask limbsRagdollMask;
    }
}