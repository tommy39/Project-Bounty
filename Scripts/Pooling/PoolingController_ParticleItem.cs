using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.VFX;

namespace IND.Core.Pooling
{
    public class PoolingController_ParticleItem : IND_Mono
    {
        [InlineEditor] public PoolingData_Generic poolingData;
        public List<PoolableParticleItem> createdPoolableItems = new List<PoolableParticleItem>();
        public override void Init()
        {
            for (int i = 0; i < poolingData.amountToSpawn; i++)
            {
                PoolableParticleItem newItem = new PoolableParticleItem();
                newItem.geo = Instantiate(poolingData.objectToPoolPrefab);
                newItem.geo.transform.SetParent(transform);
                newItem.geo.transform.localPosition = Vector3.zero;
                newItem.particleController = newItem.geo.GetComponent<GenericParticleController>();
                createdPoolableItems.Add(newItem);
                newItem.particleController.ClearOnStart(this);
            }
        }

        public GenericParticleController GetPoolableObject()
        {
            if (createdPoolableItems.Count == 0)
            {
                Debug.LogError(gameObject.name + " Has no available objects in the pool currently, increase the amount to spawn");
                return null;
            }

            GenericParticleController particleController = createdPoolableItems[0].particleController;
            particleController.transform.SetParent(null);
            particleController.PlayParticles();
            createdPoolableItems.RemoveAt(0);
            return particleController;
        }



        public void ReIntergratePoolableObject(GenericParticleController particleController)
        {
            PoolableParticleItem newItem = new PoolableParticleItem();
            newItem.geo = particleController.gameObject;
            newItem.particleController = particleController;

            createdPoolableItems.Add(newItem);
            particleController.transform.SetParent(transform);
            particleController.transform.localPosition = Vector3.zero;
            particleController.transform.localRotation = Quaternion.identity;
        }
    }

    [System.Serializable]
    public class PoolableParticleItem
    {
        public GameObject geo;
        public GenericParticleController particleController;
    }

}