using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Inventory
{
    [System.Serializable]
    public class PlayerInventoryAmmo : IND_Base
    {
        public int rifleAmmo;
        public int pistolAmmo;
        public int shotgunShells = 2;
        public int arrows = 20;
    }
}
