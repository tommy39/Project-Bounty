using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using IND.Core.Missions;
using IND.Core.Managers;
using IND.Core.Challenges;

namespace IND.Core.UI.Challenges
{
    [System.Serializable]
    public class UI_ChallengesController : MonoBehaviour
    {
        public GameObject challengeTemplatePrefab;
        public Transform challengesList;
        private ChallengeManager challengeManager;
        private List<UI_ChallengeItem> createdChallengeObjects = new List<UI_ChallengeItem>();

        public void Init()
        {
            challengeManager = FindObjectOfType<ChallengeManager>();
        }

        public void FirstTimeSetup()
        {
            for (int i = 0; i < challengeManager.activeChalleneges.Count; i++)
            {
                GameObject geo = Instantiate(challengeTemplatePrefab, challengesList);
                UI_ChallengeItem challengeItem = geo.GetComponent<UI_ChallengeItem>();
                challengeItem.CreateNewItem(challengeManager.activeChalleneges[i]);
                createdChallengeObjects.Add(challengeItem);
            }
        }

        public void UpdateChallenges()
        {
            for (int i = 0; i < createdChallengeObjects.Count; i++)
            {
                createdChallengeObjects[i].UpdateItem();
            }
        }
    }
}