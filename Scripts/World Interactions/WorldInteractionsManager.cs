using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    public class WorldInteractionsManager : IND_Mono
    {
        public List<WorldInteractionController> interactions = new List<WorldInteractionController>();
        public override void Init()
        {
            interactions.Clear();

            WorldInteractionController[] interactionsArray = FindObjectsOfType<WorldInteractionController>();
            foreach (WorldInteractionController item in interactionsArray)
            {
                interactions.Add(item);
            }
        }

        public override void Tick()
        {
            for (int i = 0; i < interactions.Count; i++)
            {
                interactions[i].Tick();
            }
        }

        public void AddNewInteraction(WorldInteractionController controller)
        {
            interactions.Add(controller);
            controller.Init();
        }

        public void RemoveInteraction(WorldInteractionController controller)
        {
            interactions.Remove(controller);
        }

        public static WorldInteractionsManager singleton;
        private void Awake()
        {
            singleton = this;
        }

    }
}
