using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    public class WorldInteractionParentIdentifier : IND_Mono
    {
        public static WorldInteractionParentIdentifier singleton;
        private void Awake()
        {
            singleton = this;
            
        }
    }
}