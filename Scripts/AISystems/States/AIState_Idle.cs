using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.States.Idle;

namespace IND.Core.AISystems.States
{
    [System.Serializable]
    public class AIState_Idle
    {
        private AIStateController stateController;

        public AIIdleStateType idleStateType;
        [ShowIf("idleStateType", AIIdleStateType.STAY_STILL)] public IdleState_StayStill stayStillIdle;
        [ShowIf("idleStateType", AIIdleStateType.LOOK_AT_TARGETS)] public IdleState_LookAtTarget lookAtTargetIdle;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;

            switch (idleStateType)
            {
                case AIIdleStateType.STAY_STILL:
                    stayStillIdle.Init(stateCon);
                    break;
                case AIIdleStateType.LOOK_AT_TARGETS:
                    lookAtTargetIdle.Init(stateCon);
                    break;
                case AIIdleStateType.ROTATE_BACK_AND_FORWARD:
                    break;
                default:
                    break;
            }
        }

        public void Tick()
        {
            switch (idleStateType)
            {
                case AIIdleStateType.STAY_STILL:                 
                    break;
                case AIIdleStateType.LOOK_AT_TARGETS:
                    lookAtTargetIdle.Tick();
                    break;
                case AIIdleStateType.ROTATE_BACK_AND_FORWARD:
                    break;
                default:
                    break;
            }
        }
        public void OnStateEnter()
        {

        }

        public void OnStateExit()
        {

        }
    }
}