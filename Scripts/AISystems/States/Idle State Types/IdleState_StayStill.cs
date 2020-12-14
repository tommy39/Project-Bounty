using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.States;
using IND.Core.Characters.Animations;

namespace IND.Core.AISystems.States.Idle
{
    [System.Serializable]
    public class IdleState_StayStill
    {
        private AIStateController stateController;
        public AISpecialIdleStateType stayStillIdleType;
        [ShowIf("stayStillIdleType", AISpecialIdleStateType.PLAY_TARGET_ANIMATION)] public AISpecialIdleStateTargetAnimType targetAnimType;

        private AnimationController animController;
        public void Init(AIStateController stateCon)
        {
            stateController = stateCon;
            animController = stateController.GetComponent<AnimationController>();

            switch (stayStillIdleType)
            {
                case AISpecialIdleStateType.STAND_STILL:
                    break;
                case AISpecialIdleStateType.PLAY_TARGET_ANIMATION:
                    PlaySpecialAnimation();
                    break;
            }
        }

        public void PlaySpecialAnimation()
        {
            switch (targetAnimType)
            {
                case AISpecialIdleStateTargetAnimType.BREATHING:
                    animController.PlayAnimationHash(animController.animData.idle_BreathingAnimationClass);
                    break; 
            }
        }
    }
}