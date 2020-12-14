using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class MoveUIElementToMousePos
    {      
        public static void Function(Vector3 currentMousePos, RectTransform gameObjectToMove, Vector3 offsetValue)
        {
            Vector3 newPos = currentMousePos + offsetValue;
            gameObjectToMove.position = newPos;
        }

    }
}
