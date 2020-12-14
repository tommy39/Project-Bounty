using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems.LookAtTargetsNetwork
{
    public class LookAtTargetsNetworkController : IND_Mono
    {
        [InlineEditor] public List<LookAtTargetItem> targets = new List<LookAtTargetItem>();

        [Button]
        void GetAllChildTargets()
        {
            targets.Clear();
            LookAtTargetItem[] items = GetComponentsInChildren<LookAtTargetItem>();
            foreach (LookAtTargetItem item in items)
            {
                targets.Add(item);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < targets.Count; i++)
            {
                if (i != targets.Count - 1)
                {
                    Gizmos.DrawLine(targets[i].transform.position, targets[i + 1].transform.position);
                }

            }


        }
    }
}