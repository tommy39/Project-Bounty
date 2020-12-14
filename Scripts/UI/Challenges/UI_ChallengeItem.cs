using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using IND.Core.Missions;
using IND.Core.Challenges;

namespace IND.Core.UI.Challenges
{
    public class UI_ChallengeItem : IND_Mono
    {
        public TextMeshProUGUI objectiveText;
        public TextMeshProUGUI completionText;
        private ChallengeRuntime challengeRuntime;
        private ChallengeData challengeData;

        public void CreateNewItem(ChallengeRuntime challenge)
        {
            challengeRuntime = challenge;
            challengeData = challenge.challengeData;
            UpdateItem();
        }

        public void UpdateItem()
        {
            challengeRuntime.UpdateUIInformation(this);
        }
    }
}