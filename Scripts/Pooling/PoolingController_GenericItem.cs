using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Pooling
{
    public class PoolingController_GenericItem : IND_Mono
    {
        [InlineEditor] public PoolingData_Generic poolingData;

        public List<GameObject> readyToUseObjects = new List<GameObject>();

        public override void Init()
        {
            for (int i = 0; i < poolingData.amountToSpawn; i++)
            {
                GameObject geo = Instantiate(poolingData.objectToPoolPrefab, transform);
                readyToUseObjects.Add(geo);
            }
        }
        private void Start()
        {
            for (int i = 0; i < readyToUseObjects.Count; i++)
            {
                readyToUseObjects[i].transform.SetParent(gameObject.transform);
                readyToUseObjects[i].transform.localPosition = Vector3.zero;
            }
        }

        public GameObject GetPoolableObject()
        {
            if(readyToUseObjects.Count == 0)
            {
                Debug.LogError(gameObject.name + " Has no available objects in the pool currently, increase the amount to spawn");
                return null;
            }

            GameObject geo = readyToUseObjects[0];
            geo.transform.SetParent(null);
            readyToUseObjects.RemoveAt(0);
            return geo;
        }



        public void ReIntergratePoolableObject(GameObject geo)
        {
            readyToUseObjects.Add(geo);
            geo.transform.SetParent(transform);
            geo.transform.localPosition = Vector3.zero;
       //     geo.transform.localRotation = Quaternion.identity;
        }
        public override void Tick()
        {

        }
    }
}