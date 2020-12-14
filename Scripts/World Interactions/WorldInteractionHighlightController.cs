using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.UI.WorldInteractions;
using IND.Core.Shared.Statics;
using UnityEngine.UI;
using IND.Core.Cameras;

namespace IND.Core.WorldInteractions
{
    public class WorldInteractionHighlightController : IND_Mono
    {
        [InlineEditor] public WorldInteractionData worldInteractionData;
        public RectTransform createdUIObject;
        private Camera cameraRef;
        [HideInInspector] public UI_WorldInteractionManager interactionUI;
        public override void Init()
        {
            cameraRef = FindObjectOfType<CamControllerGameplay>().GetComponent<Camera>();
            interactionUI = UI_WorldInteractionManager.singleton;
            SpawnInteractionHighlightUIObject();
        }

        void SpawnInteractionHighlightUIObject()
        {
            GameObject geo = Instantiate(interactionUI.managerData.distantObjectPrefab);
            geo.transform.SetParent(interactionUI.transform);
            createdUIObject = geo.GetComponent<RectTransform>();
        }

        public override void Tick()
        {
            createdUIObject.transform.localPosition = GeteUIObjectToWorldPos.GetScreenPointOverlayUI(cameraRef, interactionUI.parentCanvas, transform.position, worldInteractionData.hudOffset);
        }
    }
}