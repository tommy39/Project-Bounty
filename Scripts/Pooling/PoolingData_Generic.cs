using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Pooling
{
    [CreateAssetMenu(fileName = "Pooling Data Generic", menuName = "IND/Core/Pooling/Pooling Data Generic")]
    public class PoolingData_Generic : ScriptableObject
    {
        public GameObject objectToPoolPrefab;
        public int amountToSpawn;
    }
}