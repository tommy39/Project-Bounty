using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.LimbGibbings;

namespace IND.Core.Characters.Animations
{
    public class AnimatorHook : IND_Mono
    {
        [HideInInspector] public AnimationController animController;
        public Animator anim;
        public override void Init()
        {
            anim = GetComponent<Animator>();
            animController = GetComponentInParent<AnimationController>();
        }

        public override void Tick()
        {

        }

        public override void FixedTick()
        {

        }

        public void EndFiring()
        {
            animController.SetAnimBool(animController.animData.isFiringBool, false);
        }

        public virtual void ReloadCompleted()
        {

        }

        public virtual void DropMagazineDuringReload()
        {

        }

        public virtual void OpenMeleeAttackColliders() { }
        public virtual void CloseMeleeAttackColliders() { }
        public virtual void OpenFistsAttackColliders() { }
        public virtual void CloseFistsAttackColliders() { }
        public virtual void EndFistsAttack() { }
        public virtual void EndMeleeAttack() { }

        public virtual void DisableAnimator()
        {
            animController.DisableAnimator();
        }

        public virtual void EnableRagdoll()
        {
            LimbsController limbController = GetComponentInParent<LimbsController>();
            limbController.EnableEntireRagdoll();
        }
    }
}