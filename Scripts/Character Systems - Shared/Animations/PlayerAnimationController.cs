using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Animations
{
    public class PlayerAnimationController : AnimationController
    {
        [HideInInspector] public PlayerAnimationData playerAnimData;

        private ScriptableBool isPressingMovementKeysInput;
        private ScriptableFloat verticalNormalizedMoveAmount;
        private ScriptableFloat horizontalNormalizedMoveAmount;
        public override void Init()
        {
            base.Init();

            playerAnimData = Resources.Load("Player/Animations/PlayerAnimationData") as PlayerAnimationData;
            isPressingMovementKeysInput = Resources.Load("Player/Movement/Player - Is Pressing Movement Keys") as ScriptableBool;
            verticalNormalizedMoveAmount = Resources.Load("Player/Movement/Player - Normalized Movement Vertical") as ScriptableFloat;
            horizontalNormalizedMoveAmount = Resources.Load("Player/Movement/Player - Normalized Movement Horizontal") as ScriptableFloat;

            playerAnimData.ConvertAnimsToHash();
            SetAnimBool(animData.isPlayerBool, true);
        }

        public override void Tick()
        {
            SetAnimBool(playerAnimData.isPressingMovementKeysBool, isPressingMovementKeysInput.value);
            SetAnimFloat(animData.verticalFloat, verticalNormalizedMoveAmount.value);
            SetAnimFloat(animData.horizontalFloat, horizontalNormalizedMoveAmount.value);
        }

  
    }
}