using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class GetLayerFromLayerMask
    {
        public static int GetInt(LayerMask layerMask)
        {
            int layerNumber = 0;
            int layer = layerMask.value;
            while (layer > 0)
            {
                layer = layer >> 1;
                layerNumber++;
            }
            return layerNumber - 1;
        }
    }
}