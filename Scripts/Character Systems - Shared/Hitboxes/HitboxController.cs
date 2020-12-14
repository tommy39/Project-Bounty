using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Hitboxes
{
    public class HitboxController : IND_Mono
    {
        private SubHitboxController[] allHitboxes;
        public override void Init()
        {
            allHitboxes = GetComponentsInChildren<SubHitboxController>();
            foreach (SubHitboxController item in allHitboxes)
            {
                item.Init();
            }
        }

        public void DisableHitboxes()
        {
            foreach (SubHitboxController item in allHitboxes)
            {
                item.collision.enabled = false;
            }
        }

        public void EnableHitboxes()
        {
            foreach (SubHitboxController item in allHitboxes)
            {
                item.collision.enabled = true;
            }
        }
    }
}