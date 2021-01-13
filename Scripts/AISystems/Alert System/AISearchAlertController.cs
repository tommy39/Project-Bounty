using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.States;

namespace IND.Core.AISystems
{
    public class AISearchAlertController : IND_Mono
    {
        AIStateController stateController;

        public override void Init()
        {
            stateController = GetComponent<AIStateController>();
        }

        public void SearchAreaNotifcationRecieved(Vector3 positionToSearch)
        {
            //  stateController.searchState.AssignSearchTask(positionToSearch);

            PlayerEnteredFOV();
        }

        void PlayerEnteredFOV()
        {
            if (stateController.currentState != AIStateType.COMBAT)
            {
                if (stateController.currentState == AIStateType.KNOCKEDDOWN)
                    return;

                stateController.ChangeState(AIStateType.COMBAT);
            }
        }
    }
}