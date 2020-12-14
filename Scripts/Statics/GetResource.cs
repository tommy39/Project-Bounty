using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core
{
    public static class GetResource
    {
        public static ScriptableFloat DeltaTimer()
        {
            ScriptableFloat deltaTimer = Resources.Load("Core Variables/DeltaTimer") as ScriptableFloat;
            return deltaTimer;
        }
    }
}