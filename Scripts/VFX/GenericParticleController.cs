using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Pooling;

namespace IND.Core.VFX
{
    public class GenericParticleController : IND_Mono
    {
        public float reloadParticleTime = 2f;
        public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        private PoolingController_ParticleItem particlePoolingController;
        public void ClearOnStart(PoolingController_ParticleItem poolController)
        {
            particlePoolingController = poolController;
            ReloadParticle();
        }


        [Button]
        void GetParticleSystems()
        {
            particleSystems.Clear();

            ParticleSystem[] subParticles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem item in subParticles)
            {
                particleSystems.Add(item);
            }
        }

        public void PlayParticles()
        {


            for (int i = 0; i < particleSystems.Count; i++)
            {
                particleSystems[i].Play();
            }
            StartCoroutine("ReloadParticleTimer");
        }

        void ReloadParticle()
        {
            for (int i = 0; i < particleSystems.Count; i++)
            {
                particleSystems[i].Stop();
                particleSystems[i].Clear();
            }

            particlePoolingController.ReIntergratePoolableObject(this);
        }

        IEnumerator ReloadParticleTimer()
        {
            yield return new WaitForSeconds(reloadParticleTime);
            ReloadParticle();
        }

        private void Reset()
        {
            GetParticleSystems();
        }
    }
}