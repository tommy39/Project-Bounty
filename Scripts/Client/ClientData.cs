using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Client
{
    [CreateAssetMenu(fileName = "ClientData", menuName = "IND/Core/Client/Client Data")]
    public class ClientData : ScriptableObject
    {
        public int clientID = 1;
        public ClientType clientType;
        public int currentUnlockPointsCampaign = 0;
    }
}