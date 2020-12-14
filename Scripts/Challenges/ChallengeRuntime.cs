using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.UI.Challenges;
using IND.Core.Client;

namespace IND.Core.Challenges
{
    public class ChallengeRuntime : IND_Mono
    {
        [InlineEditor] public ChallengeData challengeData;
        public bool challengeCompleted = false;
        [HideInInspector] public string completionTextFirstPart;
        [HideInInspector] public string completionTextLastPart;

        public override void Init()
        {

        }

        public override void Tick()
        {
        }

        public virtual void CheckCompletionStateAtRuntime()
        {

        }

        public void ChallenegeCompleted()
        {
            challengeCompleted = true;
            ClientData clientData = FindObjectOfType<ClientManager>().clientData;
            clientData.currentUnlockPointsCampaign += challengeData.challengeUnlockPoints;
        }

        public virtual void UpdateUIInformation(UI_ChallengeItem challengeItem)
        {           

            challengeItem.completionText.text = completionTextFirstPart + "/" + completionTextLastPart;
            challengeItem.objectiveText.text = challengeData.challenegeObjective;
        }
    }
}