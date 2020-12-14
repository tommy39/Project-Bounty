using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.States;
using IND.Core.Characters.Animations;
using IND.Core.AISystems.LookAtTargetsNetwork;

namespace IND.Core.AISystems.States.Idle
{
    [System.Serializable]
    public class IdleState_RotateBackAndForward
    {
        private AIStateController stateController;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
        }

        public void Tick()
        {
           
        }

    }
}