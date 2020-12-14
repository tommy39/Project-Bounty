using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Abilities
{
    [CreateAssetMenu(fileName = "Ability Base", menuName = "IND/Core/Player/Abilities/AbilityBase")]
    public class Ability : ScriptableObject
    {
        public string abilityName;
        public AbilityInputType inputType;

        [HideInInspector] public AbilityController abilityController;

        public virtual void InitAbility(AbilityController controller)
        {
            abilityController = controller;
        }

        public virtual void StartAbilityUsage()
        {

        }

        public virtual void StopAbilityUsage()
        {

        }

        public virtual void TickAbility()
        {

        }

        public virtual void FixedTickAbility()
        {

        }
    }
}