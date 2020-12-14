using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    public class WeaponMuzzleFlashController : IND_Mono
    {
        [Required] public ParticleSystem particleObj;
        public override void Init()
        {
            particleObj.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }


        [Button]
        private void Setup()
        {
            particleObj = GetComponentInChildren<ParticleSystem>();
        }

        [Button]
        public void PlayParticle()
        {
            particleObj.Play();
        }

        private void Reset()
        {
            Setup();
        }
    }
}