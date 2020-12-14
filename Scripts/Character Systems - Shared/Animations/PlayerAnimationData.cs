using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Animations
{
    [CreateAssetMenu(fileName = "PlayerAnimationData", menuName = "IND/Core/Character/Animation/PlayerAnimationData")]
    public class PlayerAnimationData : ScriptableObject
    {
        public int isPressingMovementKeysBool;

        public int isInActionBool;

        public int isRollingBool;
        public int slideForwardAnimClass;

        public int executionSlashAnimClass;
        public int executionCrouchMountAnimClass;
        public void ConvertAnimsToHash()
        {
            isPressingMovementKeysBool = Animator.StringToHash("IsPressingMovementKeys");
            isRollingBool = Animator.StringToHash("IsRolling");
            slideForwardAnimClass = Animator.StringToHash("Slide_Forward");
            isInActionBool = Animator.StringToHash("IsInAction");

            executionSlashAnimClass = Animator.StringToHash("Execution_Test_Slash");
            executionCrouchMountAnimClass = Animator.StringToHash("Execution_Test_CrouchMount");
        }
    }
}