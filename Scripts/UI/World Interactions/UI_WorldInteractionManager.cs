using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace IND.Core.UI.WorldInteractions
{
    public class UI_WorldInteractionManager : IND_Mono
    {
        [InlineEditor] public UI_WorldInteractionManagerData managerData;
        public RectTransform parentCanvas;
        public override void Init()
        {
            parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }

        public override void Tick()
        {

        }

        public static UI_WorldInteractionManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}