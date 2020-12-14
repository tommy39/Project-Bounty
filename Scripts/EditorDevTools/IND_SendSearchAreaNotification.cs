using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;

namespace IND.Core.DevTools
{
    public class IND_SendSearchAreaNotification : IND_Mono
    {
        public AISearchAlertController targetAI;

        [Button]
        public void SendSearchNotification()
        {
            targetAI.SearchAreaNotifcationRecieved(transform.position);
        }
    }
}