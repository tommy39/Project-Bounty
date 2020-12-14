using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    [System.Serializable]
    public static class GetRollValue
    {
        public static bool Execute(int minValue, int maxValue, int successChance)
        {
            int rollValue = Random.Range(minValue, maxValue);

            if (rollValue <= successChance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

