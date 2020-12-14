using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    [System.Serializable]
    public class WeaponRigidbodyData : IND_Base
    {
        public float mass = 1f;
        public float drag = 0f;
        public float angularDrag = 0.5f;
    }
}
