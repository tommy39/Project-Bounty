using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    public class Weapon_BulletManager : IND_Mono
    {
        public List<Weapon_FiredProjectile> bulletsToTick = new List<Weapon_FiredProjectile>();    
        public override void FixedTick()
        {

            for (int i = 0; i < bulletsToTick.Count; i++)
            {
                bulletsToTick[i].FixedTick();
            }
        }

        public static Weapon_BulletManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}