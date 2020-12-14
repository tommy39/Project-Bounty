using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace IND.Core.AISystems
{
    public class AIManager : IND_Mono
    {
        public List<AIController> allChildAI = new List<AIController>();
        public List<AIController> activeChildAI = new List<AIController>();

        [InlineEditor] public ScriptableLayerMask enemy_ToPlayerMask;
        [InlineEditor] public ScriptableLayerMask playerMask;

        public event EventHandler OnEnemyKilled;

        private AILevelContainerManager aiLevelContainerManager;
        public override void Init()
        {
            aiLevelContainerManager = FindObjectOfType<AILevelContainerManager>();

            AIController[] ai = aiLevelContainerManager.GetComponentsInChildren<AIController>();
            foreach (AIController item in ai)
            {
                allChildAI.Add(item);
                activeChildAI.Add(item);
                item.Init();
            }
        }

        public override void Tick()
        {
            for (int i = 0; i < activeChildAI.Count; i++)
            {
                activeChildAI[i].Tick();
            }
        }

        public void OnDeadAI(AIController deadAI)
        {
            OnEnemyKilled.Invoke(this, EventArgs.Empty);

            activeChildAI.Remove(deadAI);
            deadAI.transform.SetParent(aiLevelContainerManager.deadAiGroup);
        }

    }
}