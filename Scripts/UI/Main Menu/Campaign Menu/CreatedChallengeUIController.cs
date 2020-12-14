using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Challenges;
using TMPro;


namespace IND.Core.UI.MainMenu.Campaign
{
    public class CreatedChallengeUIController : IND_Mono
    {
        private ChallengeData challengeData;
        [SerializeField] private TextMeshProUGUI text;
        public void SetupChallenge(ChallengeData challenege)
        {
            challengeData = challenege;
            text.text = challenege.challengeName;
        }
    }
}