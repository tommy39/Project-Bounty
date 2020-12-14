using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.DevSettings
{
    [CreateAssetMenu(fileName = "DevGameSettings_TOM", menuName = "IND/Core/DevSettings/DevGameSettings")]
    public class DevGameSettings : ScriptableObject
    {
        [FoldoutGroup("Player")] [InlineEditor] public ScriptableBool playerUnlimitedAmmo;
        [FoldoutGroup("Player")] [InlineEditor] public ScriptableBool playerUnlimitedHealth;
        public bool allowWeaponSelectionMenu = true;
    }
}