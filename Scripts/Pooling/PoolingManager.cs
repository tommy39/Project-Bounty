using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Pooling
{
    public class PoolingManager : IND_Mono
    {
        [Required] public PoolingController_GenericItem bulletPooler;
        [Required] public PoolingController_GenericItem arrowPooler;

        [Required] public PoolingController_ParticleItem hitEffectConcrete;
        [Required] public PoolingController_ParticleItem hitEffectDirt;
        [Required] public PoolingController_ParticleItem hitEffectWood;
        [Required] public PoolingController_ParticleItem hitEffectMetal;
        [Required] public PoolingController_ParticleItem hitEffectBlood;
        [Required] public PoolingController_ParticleItem hitEffectGlass;

        public List<PoolingController_GenericItem> genericPoolers = new List<PoolingController_GenericItem>();
        public override void Init()
        {
            bulletPooler.Init();
            arrowPooler.Init();

            hitEffectConcrete.Init();
            hitEffectDirt.Init();
            hitEffectWood.Init();
            hitEffectMetal.Init();
            hitEffectBlood.Init();
            hitEffectGlass.Init();

            for (int i = 0; i < genericPoolers.Count; i++)
            {
                genericPoolers[i].Init();
            }
        }

        public override void Tick()
        {
           
        }

        public static PoolingManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}