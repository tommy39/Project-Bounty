using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.UI.WorldInteractions
{
    [CreateAssetMenu(fileName = "UI_WorldInteractionManagerData", menuName = "IND/Core/UI/World Interactions/Data")]
    public class UI_WorldInteractionManagerData : ScriptableObject
    {
        public GameObject distantObjectPrefab;
    }
}