using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class GetClosestTransformFromList
    {

        /// <summary>
        /// Takes a lists of targets and compares them to the current position to find the closests
        /// </summary>
        public static Transform Function(List<Transform> targets, Vector3 currentPosition) 
        {

            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach (Transform potentialTarget in targets)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            return bestTarget;
        }

    }
}
