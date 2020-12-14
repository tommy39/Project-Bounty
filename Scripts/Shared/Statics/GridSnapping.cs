using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class GridSnapping
    {
        public static int gridSize = 1; //Keep as 1

        public static Vector3 GetNearestGridPoint(Vector3 currentPosition)
        {
            Vector3 newPos = new Vector3(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y), Mathf.RoundToInt(currentPosition.z));

            return newPos;
        }
    }
}
