using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;

namespace IND.Core.Characters.LimbGibbings
{
    public class SubLimbController : IND_Mono
    {
        public HitboxType limbType;
        public GameObject meshPrefabToSpawn;
        public List<GameObject> linkedGEO = new List<GameObject>();
        public List<RagdollComponentController> ragdollElementsToDestroy = new List<RagdollComponentController>();
        private LimbsController limbsController;
        public override void Init()
        {
            limbsController = GetComponentInParent<LimbsController>();
            
        }

        [Button]
        public void DetachLimb()
        {
            GameObject createdGEO = Instantiate(meshPrefabToSpawn);
            createdGEO.transform.position = limbsController.transform.position;
            for (int i = 0; i < linkedGEO.Count; i++)
            {
                linkedGEO[i].SetActive(false);
            }

            Transform[] childTransformsInCharacter = GetComponentsInChildren<Transform>();
            Transform[] childTransformsInCreatedMesh = createdGEO.GetComponentsInChildren<Transform>();

            foreach (Transform item in childTransformsInCreatedMesh)
            {
                foreach (Transform childs in childTransformsInCharacter)
                {
                    if(item.name == childs.name)
                    {
                        item.localPosition = childs.localPosition;
                        item.localRotation = childs.localRotation;
                    }
                }
            }

            for (int i = 0; i < ragdollElementsToDestroy.Count; i++)
            {
                ragdollElementsToDestroy[i].DestroyRagdollComponent(limbsController);
            }
        }

     
    }
}