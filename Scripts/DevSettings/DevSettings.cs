using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.DevSettings
{
    [CreateAssetMenu(fileName = "DevSettings_TOM", menuName = "IND/Core/DevSettings/DevSettings")]
    public class DevSettings : ScriptableObject
    {
        [InlineEditor] public DevEditorSettings editorSettings;
        [InlineEditor] public DevGameSettings gameSettings;
    }
}