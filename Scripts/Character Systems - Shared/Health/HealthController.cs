using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using IND.Core.AISystems;
using IND.Core.AISystems.Movement;
using IND.Core.Characters.Animations;
using IND.Core.HitEffects;

namespace IND.Core.Characters.Health
{
    public class HealthController : IND_Mono
    {

        [Required] [InlineEditor] public HealthData healthData;
        [HideInInspector] public int currentHealth;
        [HideInInspector] public bool isDead;
        public override void Init()
        {
            SetHealthToMax();
        }


        void SetHealthToMax()
        {
            currentHealth = healthData.maxHealth;
        }

        public virtual void TakeDamage(int amount, HitboxType hitboxType)
        {
            if (currentHealth < 0)
                return;

            currentHealth -= amount;

            //Debug.Log(gameObject.name + " Took " + amount + " Damage ");

            if (currentHealth <= 0)
            {
                Death(hitboxType);
            }
        }

        public virtual void Death(HitboxType hitboxType)
        {
            isDead = true;
        }
    }
}