
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core
{
    [CreateAssetMenu(fileName = "Input Bool Action", menuName = "IND/Variables/Inputs/Input Bool Action")]
    public class ScriptableInputBoolAction : SerializedScriptableObject
    {
        [InlineEditor] public ScriptableBool value;
    }
}