using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Animations
{
    [CreateAssetMenu(fileName = "AnimationData", menuName = "IND/Core/Character/Animation/AnimationData")]
    public class AnimationData : ScriptableObject
    {

        public int isReloadingBool;
        public int isFiringBool;
        public int isBoltingBool;

        public int isMeleeAttackingBool;
        public int verticalFloat;
        public int horizontalFloat;
        public int isIdleBool;

        public int isKnockedDown;
        public int knockDownAnimationClass;

        public int weaponAnimID;

        public int death01AnimationClass;
        public int getNewWeaponAnimationClass;
        public int meleeAttackAnimationClass;

        public int idle_BreathingAnimationClass;

        public int isPlayerBool;
        public void ConvertAnimsToHash()
        {
            isReloadingBool = Animator.StringToHash("IsReloading");
            isFiringBool = Animator.StringToHash("IsFiring");
            isBoltingBool = Animator.StringToHash("IsBolting");
            weaponAnimID = Animator.StringToHash("WeaponAnimID");
            death01AnimationClass = Animator.StringToHash("Death 01");
            getNewWeaponAnimationClass = Animator.StringToHash("GetNewWeapon");
            isMeleeAttackingBool = Animator.StringToHash("IsMeleeAttacking");
            isIdleBool = Animator.StringToHash("IsIdle");
            horizontalFloat = Animator.StringToHash("Horizontal");
            verticalFloat = Animator.StringToHash("Vertical");
            isPlayerBool = Animator.StringToHash("IsPlayer");
            meleeAttackAnimationClass = Animator.StringToHash("Standing_Melee_Attack_Downward");
            idle_BreathingAnimationClass = Animator.StringToHash("Special_Idle_Breathing");
            isKnockedDown = Animator.StringToHash("IsKnockedDown");
            knockDownAnimationClass = Animator.StringToHash("Knock Down");
        }
    }
}