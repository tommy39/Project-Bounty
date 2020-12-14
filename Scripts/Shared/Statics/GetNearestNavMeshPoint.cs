using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.AI;

namespace IND.Core.Shared.Statics
{
    public static class GetNearestNavMeshPoint
    {
        public static Vector3 GetClosestNavMeshPoint(Vector3 naivePoint)
        {
            if (NavMesh.SamplePosition(naivePoint, out NavMeshHit navHit, 25f, NavMesh.AllAreas))
            {
                return navHit.position;
            }
            else
                return naivePoint;
        }
    }
}