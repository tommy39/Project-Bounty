using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.HitEffects;

namespace IND.Core.Characters.Hitboxes
{
    public class HitboxHandler : IND_Mono
    {
        public HitboxType hitboxType;
        [Required] public Rigidbody parentRigidbody;
        [InlineEditor] public List<Collider> hitboxColliders = new List<Collider>();


        [Button]
        private void GetColliders()
        {
            hitboxColliders.Clear();
            Collider[] foundColliders = GetComponentsInChildren<Collider>();
            foreach (Collider item in foundColliders)
            {
                hitboxColliders.Add(item);
                item.isTrigger = true;
            }

            MakeCollidersTriggers();
        }

        [Button]
        private void MakeCollidersTriggers()
        {
            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                hitboxColliders[i].isTrigger = true;
            }
        }

        public void AssignLayerMask(int layerNumber)
        {
            gameObject.layer = layerNumber;

            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                hitboxColliders[i].gameObject.layer = layerNumber;
            }
        }
        public void EnableColliders()
        {
            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                hitboxColliders[i].enabled = true;
            }
        }

        public void DisableColliders()
        {
            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                hitboxColliders[i].enabled = false;
            }
        }

        public void MakeSureHasEffectController()
        {
            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                if (hitboxColliders[i].gameObject.GetComponent<HittableObjectController>() == null)
                {
                    HittableObjectController control = hitboxColliders[i].gameObject.AddComponent<HittableObjectController>();
                    control.effectType = HitEffectType.BLOOD;
                }
            }
        }

        [Button]
        void AssignHandlerToColliderChildren()
        {
            for (int i = 0; i < hitboxColliders.Count; i++)
            {
                hitboxColliders[i].GetComponent<HitboxColliderHandler>().parentHandler = this;
            }
        }
    }
}