using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core
{
    [CreateAssetMenu(fileName = "StringList", menuName = "IND/Variables/StringList")]
    public class ScriptableStringsList: SerializedScriptableObject
    {
        [MultiLineProperty] public List<string> value = new List<string>();
    }
}