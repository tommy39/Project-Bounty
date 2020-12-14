using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    [System.Serializable]
    public class WeaponItemAiData : IND_Base
    {
        [PropertyTooltip("When the player is within field of fiew, the AI will only shoot at him if he is within this attack range")] [PropertyRange(0, 50)]
        public float shootRange = 10f;
        [PropertyRange(0, 3)] 
        public float attackCooldown = 1f;
        [PropertyRange(0, 20)][PropertyTooltip("The Range that the player is to the AI, when within range the AI will stop in its position")]
        public float attackRangedStoppingDistance = 1f;
        [PropertyRange(0,50)]
        public float bulletSpeedModifier = 30f;
    }
}
