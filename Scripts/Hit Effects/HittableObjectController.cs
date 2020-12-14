using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.VFX;
using IND.Core.Pooling;

namespace IND.Core.HitEffects
{
    public class HittableObjectController : IND_Mono
    {
        public HitEffectType effectType;
        public void ObjectHit(Vector3 hitPosition, Vector3 direction)
        {
            GenericParticleController particleController = null;
            switch (effectType)
            {
                case HitEffectType.WOOD:
                    particleController = PoolingManager.singleton.hitEffectWood.GetPoolableObject();
                    break;
                case HitEffectType.METAL:
                    particleController = PoolingManager.singleton.hitEffectMetal.GetPoolableObject();
                    break;
                case HitEffectType.BLOOD:
                    particleController = PoolingManager.singleton.hitEffectBlood.GetPoolableObject();
                    break;
                case HitEffectType.DIRT:
                    particleController = PoolingManager.singleton.hitEffectDirt.GetPoolableObject();
                    break;
                case HitEffectType.CONCRETE:
                    particleController = PoolingManager.singleton.hitEffectConcrete.GetPoolableObject();
                    break;
                case HitEffectType.GLASS:
                    particleController = PoolingManager.singleton.hitEffectGlass.GetPoolableObject();
                    break;
            }
            particleController.transform.position = hitPosition;
            particleController.transform.rotation = Quaternion.LookRotation(direction);
            StartCoroutine("TimerToReAddToPool", particleController);
        }

        private IEnumerator TimerToReAddToPool(GenericParticleController particleController)
        {
            
            yield return new WaitForSeconds(1.5f);
            switch (effectType)
            {
                case HitEffectType.WOOD:
                    PoolingManager.singleton.hitEffectWood.ReIntergratePoolableObject(particleController);
                    break;
                case HitEffectType.METAL:
                    PoolingManager.singleton.hitEffectMetal.ReIntergratePoolableObject(particleController);
                    break;
                case HitEffectType.BLOOD:
                    PoolingManager.singleton.hitEffectConcrete.ReIntergratePoolableObject(particleController);
                    break;
                case HitEffectType.DIRT:
                    PoolingManager.singleton.hitEffectDirt.ReIntergratePoolableObject(particleController);
                    break;
                case HitEffectType.CONCRETE:
                    PoolingManager.singleton.hitEffectConcrete.ReIntergratePoolableObject(particleController);
                    break;
                case HitEffectType.GLASS:
                    PoolingManager.singleton.hitEffectGlass.ReIntergratePoolableObject(particleController);
                    break;
            }
        }
    }
}