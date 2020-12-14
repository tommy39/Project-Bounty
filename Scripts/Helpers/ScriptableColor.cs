using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
namespace IND.Core
{
    [CreateAssetMenu(fileName = "Image", menuName = "IND/Variables/Color")]
    public class ScriptableColor : SerializedScriptableObject {

        public Color value = Color.black;
		
	}
}