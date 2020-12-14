using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class GeteUIObjectToWorldPos
    {
        public static Vector2 GetScreenPointOverlayUI(Camera cam, RectTransform canvasRect, Vector3 targetPos, Vector3 offset)
        {
            Vector2 screenPoint = cam.WorldToScreenPoint((targetPos + offset));
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);
            return canvasPos;
        }
    }
}