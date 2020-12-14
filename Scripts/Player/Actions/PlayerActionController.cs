using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Actions
{
    public class PlayerActionController : IND_Mono
    {
        private ScriptableBool isPlayerInAction;
        public override void Init()
        {
            isPlayerInAction = Resources.Load("Player/Actions/Is Player In Action") as ScriptableBool;
            isPlayerInAction.value = false;
        }

        public override void Tick()
        {

        }

        public override void FixedTick()
        {

        }
    }
}