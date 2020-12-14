using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Player.Abilities
{
    public class AbilityController : IND_Mono
    {
        [InlineEditor] public Ability currentAbility;
        public bool isAbilityActivelyExecuting = false;
        private ScriptableInputBoolAction activateAbilityAction;

        public bool isAbilititesEnabled = false;

        public override void Init()
        {
            activateAbilityAction = Resources.Load("Input Actions/Input Action Ability Toggle") as ScriptableInputBoolAction;
            currentAbility.InitAbility(this);
        }

        public override void Tick()
        {
            if (isAbilititesEnabled == false)
                return;

            if(activateAbilityAction.value.value == true)
            {
                if(isAbilityActivelyExecuting == false)
                {
                    isAbilityActivelyExecuting = true;
                    currentAbility.StartAbilityUsage();
                }
                else
                {
                    isAbilityActivelyExecuting = false;
                    currentAbility.StopAbilityUsage();
                }
            }

            if (isAbilityActivelyExecuting == true)
            {
                currentAbility.TickAbility();
            }
        }

        public override void FixedTick()
        {
            if (isAbilityActivelyExecuting == true)
            {
                currentAbility.FixedTickAbility();
            }
        }

    }
}