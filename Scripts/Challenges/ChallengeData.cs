using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Challenges
{
    [CreateAssetMenu(fileName = "Challenge Data Generic", menuName = "IND/Core/Challenges/Challenge Data Generic")]
    public class ChallengeData : ScriptableObject
    {
        public string challengeName;
        public string challenegeObjective;
        public int challengeUnlockPoints;

        public bool hasBeenCompleted = false;

        public void ChallengeInit(ChallengeManager challengeManager)
        {
            if (hasBeenCompleted == true)
            {
                challengeManager.completedChallenges.Add(this);
            }
            else
            {
                SetupChallenegeRuntime(challengeManager);
            }
        }

        public virtual void SetupChallenegeRuntime(ChallengeManager challenegeManager)
        {
            //Create Runtime Logic For Challenege

        }
    }
}