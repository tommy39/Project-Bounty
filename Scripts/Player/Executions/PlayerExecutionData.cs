using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Executions
{
    [CreateAssetMenu(fileName = "Player Execution Data", menuName = "IND/Core/Player/Execution Data")]
    public class PlayerExecutionData : ScriptableObject
    {
        [PropertyRange(0, 10)] public float maxSearchDistance = 2f;
        [Required] [InlineEditor] public ScriptableLayerMask knockedDownEnemiesLayer;
        [Required] [InlineEditor] public ScriptableBool executionInput;

    }
}