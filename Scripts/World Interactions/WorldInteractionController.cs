using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    public class WorldInteractionController : IND_Mono
    {
        [InlineEditor] public WorldInteractionData interaction;
        private WorldInteractionHighlightController highlightController;

        public override void Init()
        {
            highlightController = GetComponent<WorldInteractionHighlightController>();
            highlightController.Init();
        }

        public override void Tick()
        {
            highlightController.Tick();
        }

        public void DestroyInteractionOnly(GameObject geoToUnparent)
        {
            geoToUnparent.transform.SetParent(null);
            Destroy(highlightController.createdUIObject.gameObject);
            WorldInteractionsManager.singleton.RemoveInteraction(this);
            Destroy(gameObject);
        }
        public void DestroyWorldInteraction()
        {
            Destroy(highlightController.createdUIObject.gameObject);
            WorldInteractionsManager.singleton.RemoveInteraction(this);
            Destroy(gameObject);
        }
        public void ExecuteInteraction(InteractionSearcherController player)
        {
            interaction.executionEvent.ExecuteWorldInteraction(this, player);
        }
    }
}