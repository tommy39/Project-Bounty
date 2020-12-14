using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Audio
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] [InlineEditor] protected PlayerAudioSettings audioSettings;

        void Update()
        {
            AudioListener.volume = audioSettings.audioVolume;
        }
    }
}