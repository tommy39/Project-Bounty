using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Movement
{
    [CreateAssetMenu(fileName = "Movement Inputs", menuName = "IND/Core/Player/Movement/Movement Inputs")]
    public class PlayerMovementInputs : ScriptableObject
    {
        public ScriptableBool isPressingMovementKeys;
        public ScriptableBool rightMovementInput;
        public ScriptableBool leftMovementInput;
        public ScriptableBool forwardMovementInput;
        public ScriptableBool backwardsMovementInput;
        public ScriptableFloat horizontalInput;
        public ScriptableFloat verticalInput;
        public ScriptableBool isPlayerInAction;
    }
}