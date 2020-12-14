using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    [System.Serializable]
    public static class GetFormationUtils
    {
        public static Vector3[] GetSquareFormation(Vector3 startPos)
        {
            float offsetValue = 2f;
            Vector3[] positions = new Vector3[9];
            positions[0] = new Vector3(startPos.x, startPos.y, startPos.z); //Centre Pos
            positions[1] = new Vector3(startPos.x + offsetValue, startPos.y, startPos.z); //1 Tile Right
            positions[2] = new Vector3(startPos.x - offsetValue, startPos.y, startPos.z); //1 Tile Left
            positions[3] = new Vector3(startPos.x, startPos.y, startPos.z + offsetValue); //1 Tile Forward
            positions[4] = new Vector3(startPos.x, startPos.y, startPos.z - offsetValue); //1 Tile Backward
            positions[5] = new Vector3(startPos.x + offsetValue, startPos.y, startPos.z + offsetValue); //1 Tile Right & 1 Tile Forward
            positions[6] = new Vector3(startPos.x + offsetValue, startPos.y, startPos.z - offsetValue); //1 Tile Right & 1 Tile Backward
            positions[7] = new Vector3(startPos.x - offsetValue, startPos.y, startPos.z + offsetValue); //1 Tile Left & 1 Tile Forward
            positions[8] = new Vector3(startPos.x - offsetValue, startPos.y, startPos.z - offsetValue); //1 Tile Left & 1 Tile Backward
            return positions;
        }
    }
}
