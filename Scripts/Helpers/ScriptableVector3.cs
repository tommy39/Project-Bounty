using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core
{
    [CreateAssetMenu(fileName = "Vector3", menuName = "IND/Variables/Vector3")]
    public class ScriptableVector3 : SerializedScriptableObject {

        public Vector3 value;


    }
}