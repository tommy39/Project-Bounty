using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Animations
{
    public class AnimationController : IND_Mono
    {
        [HideInInspector] public AnimationData animData;
        [HideInInspector] public AnimatorHook animHook;
        private Animator anim;
        [HideInInspector] public AnimatorClipInfo[] currentClipInfo;
        public override void Init()
        {
            animData = Resources.Load("Player/Animations/AnimationData") as AnimationData;
            animHook = GetComponentInChildren<AnimatorHook>();
            anim = GetComponentInChildren<Animator>();
            animHook.Init();
            animData.ConvertAnimsToHash();
            SetAnimBool(animData.isPlayerBool, false);
        }
        public bool GetAnimBoolState(int id)
        {
            return anim.GetBool(id);
        }

        public void SetAnimBool(int id, bool value)
        {
            anim.SetBool(id, value);
        }
        public void SetAnimFloat(int id, float value)
        {
            anim.SetFloat(id, value);
        }

        public void SetAnimInt(int id, int value)
        {
            anim.SetInteger(id, value);
        }
        public void SetAnimFloatAdvanced(int id, float value, float dampTime, float deltaTime)
        {
            anim.SetFloat(id, value, dampTime, deltaTime);
        }

        public void SetAnimatorSpeed(float value)
        {
            anim.speed = value;
        }

        public void PlayAnimationHash(int id)
        {
            anim.Play(id);
        }
        public void PlayAnimationString(string id)
        {
            anim.Play(id);
        }
        public void PlayAnimationStringAtSpecificTime(string id, int Layer, float normalizedTime)
        {
            anim.Play(id, Layer, normalizedTime);
        }
        public void EnableRootMotion()
        {
            anim.applyRootMotion = true;
        }
        public void DisableRootMotion()
        {
            anim.applyRootMotion = false;
        }

        public float GetCurrentAnimationTime(int LayerIndex)
        {
            AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(LayerIndex);

            //Fetch the current Animation clip
            currentClipInfo = animHook.anim.GetCurrentAnimatorClipInfo(LayerIndex);
            return animState.normalizedTime % 1;
        }

        public AnimationClip GetCurrentAnimationClip(int LayerIndex)
        {
            AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(LayerIndex);
            currentClipInfo = anim.GetCurrentAnimatorClipInfo(LayerIndex);

            return currentClipInfo[0].clip;
        }

        public void DisableAnimator()
        {
            anim.enabled = false;
        }
    }
}