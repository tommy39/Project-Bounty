using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Cameras
{
    [CreateAssetMenu(fileName = "Camera CinemaMachine Data", menuName = "IND/Core/Game Camera/Camera CinemaMachine Data")]
    public class CameraCinemaMachineData : ScriptableObject
    {
        [Required] public ScriptableBool leftShiftInput;

        public float focusPointMoveSpeedRegular = 5f;
        public float focusPointMoveSpeedDeadzone2 = 15f;
        public float focusPointFastReturnSpeed = 20f;

        public float deadZone1Distance = 2f;
        public float deadZone2Distance = 8f;

        public bool followPlayer = true;
        public bool confineCameraToLevelBounds = false;
    }
}