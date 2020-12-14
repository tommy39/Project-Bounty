using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player
{
    public class PlayerManager : IND_Mono
    {
        [HideInInspector] public PlayerController playerController;
        [Required] [InlineEditor] public ScriptableBool tickPlayer;
        public override void Init()
        {
            playerController = FindObjectOfType<PlayerController>();
            playerController.Init();
            tickPlayer.value = true;
        }

        public override void Tick()
        {
            if (!tickPlayer.value)
                return;

            playerController.Tick();
        }

        public override void FixedTick()
        {
            if (!tickPlayer.value)
                return;

            playerController.FixedTick();
        }
    }
}