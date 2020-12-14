using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Movement
{
    [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "IND/Core/Player/Movement/MovementSettings")]
    public class PlayerMovementSettings : ScriptableObject
    {

        public ScriptableBool isPressingMovementKeys;

        [InlineEditor] public ScriptableBool rightMovementInput;
        [InlineEditor] public ScriptableBool leftMovementInput;
        [InlineEditor] public ScriptableBool forwardMovementInput;
        [InlineEditor] public ScriptableBool backwardsMovementInput;

        public ScriptableFloat horizontalInput;
        public ScriptableFloat verticalInput;

        #region Fixed Rotation Value Positions
        [HideInInspector] public Vector3 topForwardMovement = new Vector3(0, 0, 0);
        [HideInInspector] public Vector3 topRightMovement = new Vector3(0, 45, 0);
        [HideInInspector] public Vector3 rightMovement = new Vector3(0, 90, 0);
        [HideInInspector] public Vector3 bottomRightMovement = new Vector3(0, 135, 0);
        [HideInInspector] public Vector3 bottomMovement = new Vector3(0, 180, 0);
        [HideInInspector] public Vector3 bottomLeftMovement = new Vector3(0, 225, 0);
        [HideInInspector] public Vector3 leftMovement = new Vector3(0, 270, 0);
        [HideInInspector] public Vector3 topLeftMovement = new Vector3(0, 315, 0);
        #endregion
    }
}