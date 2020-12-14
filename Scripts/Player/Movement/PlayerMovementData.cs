using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Movement
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "IND/Core/Player/Movement/MovementData")]
    public class PlayerMovementData : ScriptableObject
    {
        public float moveSpeed = 5f;
        [InlineEditor] public ScriptableFloat movementRotationSpeed;
    }
}