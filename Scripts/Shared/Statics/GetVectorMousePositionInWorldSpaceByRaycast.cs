using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class GetVectorMousePositionInWorldSpaceByRaycast
    {
        public static Vector3 Function(Camera gameCam) 
        {

            RaycastHit hit;
            Ray ray = gameCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 pos = hit.point;
                return pos;
            }
            return Vector3.zero;
        }

    }
}
