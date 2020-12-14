using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Audio
{
    [CreateAssetMenu(fileName = "Audio Settings", menuName = "IND/Core/Player/Audio Settings")]
    public class PlayerAudioSettings : ScriptableObject
    {
        [Range(0,1)]
        public float audioVolume;
    }
}