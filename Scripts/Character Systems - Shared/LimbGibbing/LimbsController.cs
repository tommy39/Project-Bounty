using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;
using IND.Core.Shared.Statics;

namespace IND.Core.Characters.LimbGibbings
{
    public class LimbsController : IND_Mono
    {
        private SubLimbController headLimb;
        private SubLimbController upperBodyLimb;
        private SubLimbController lowerBodyLimb;
        private SubLimbController rightArmLimb;
        private SubLimbController leftArmLimb;

        public List<Rigidbody> rigidBodiesToDisableOnStart = new List<Rigidbody>();
        [HideInInspector] public List<RagdollComponentController> ragdollComponentControllers = new List<RagdollComponentController>();
        public override void Init()
        {

            SubLimbController[] allSubLimbs = GetComponentsInChildren<SubLimbController>();
            foreach (SubLimbController item in allSubLimbs)
            {
                switch (item.limbType)
                {
                    case HitboxType.HEAD:
                        headLimb = item;
                        break;
                    case HitboxType.UPPER_BODY:
                        upperBodyLimb = item;
                        break;
                    case HitboxType.L_ARM:
                        leftArmLimb = item;
                        break;
                    case HitboxType.R_ARM:
                        rightArmLimb = item;
                        break;
                    case HitboxType.PROP:
                        break;
                    case HitboxType.LOWER_BODY:
                        lowerBodyLimb = item;
                        break;
                }

                item.Init();
            }

            ScriptableLayerMask ragdollLayer = Resources.Load("LayerMasks/LayerMask_Ragdoll") as ScriptableLayerMask;
            int layerMaskNumber = GetLayerFromLayerMask.GetInt(ragdollLayer.value);

            RagdollComponentController[] allRagdollComponents = GetComponentsInChildren<RagdollComponentController>();
            foreach (RagdollComponentController item in allRagdollComponents)
            {
                ragdollComponentControllers.Add(item);
                item.Init();
                item.gameObject.layer = layerMaskNumber;
            }
        }
        public void OnLimbHit(HitboxType type)
        {
            switch (type)
            {
                case HitboxType.HEAD:
                    headLimb.DetachLimb();
                    break;
                case HitboxType.UPPER_BODY:
         //           upperBodyLimb.DetachLimb();
                    break;
                case HitboxType.L_ARM:
                    leftArmLimb.DetachLimb();
                    break;
                case HitboxType.R_ARM:
                    rightArmLimb.DetachLimb();
                    break;
                case HitboxType.PROP:
                    break;
                case HitboxType.LOWER_BODY:
                    lowerBodyLimb.DetachLimb();
                    break;
            }
        }

        public void EnableEntireRagdoll()
        {
            for (int i = 0; i < ragdollComponentControllers.Count; i++)
            {
                ragdollComponentControllers[i].EnableRagdoll();
            }
        }

        public void DisableEntireRagdoll()
        {
            for (int i = 0; i < ragdollComponentControllers.Count; i++)
            {
                ragdollComponentControllers[i].DisableRagdoll();
            }
        }
    }
}