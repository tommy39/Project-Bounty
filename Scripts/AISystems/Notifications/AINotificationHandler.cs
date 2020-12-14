using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems
{
    public class AINotificationHandler : IND_Mono
    {
       [HideInInspector] public AISearchAlertController searchAlertController;

        public override void Init()
        {
            base.Init();
            GetController();
        }

        private void GetController()
        {
            searchAlertController = GetComponentInParent<AISearchAlertController>();
        }

    }
}