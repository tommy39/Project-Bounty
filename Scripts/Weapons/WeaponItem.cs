using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    [CreateAssetMenu(fileName = "Weapon_Type_Name", menuName = "IND/Core/Weapons/WeaponItemBase")]
    public class WeaponItem : ScriptableObject
    {
        [Title("BASIC WEAPON SETTINGS")] [FoldoutGroup("WEAPON SETUP")] public string weaponName;
        [FoldoutGroup("WEAPON SETUP")] public WeaponType weaponType;
        [FoldoutGroup("WEAPON SETUP")] public int weaponAnimID = 1;
        [FoldoutGroup("WEAPON SETUP")] public GameObject weaponPrefab;


        [PropertyRange(0, 1)] [Title("ATTACK SETTINGS")] [FoldoutGroup("Attack Data")] public float attackRate;
        [PropertyRange(0, 200)] [FoldoutGroup("Attack Data")] public int attackDamage = 200;
        [PropertyRange(0, 100)] [FoldoutGroup("Attack Data")] public int limbBreakThreshold = 100;

        [Title("DROP TO WORLD SETTINGS")] [PropertySpace] public int throwDistanceForce = 500;
        public bool isLethalWhenThrown = true;
        public float knockdownCollidersDisableTimer = 3f;


        [Title("SHARED WEAPON DATA")] [PropertyOrder(900)] [PropertySpace] [InlineEditor] public WeaponItemSharedData sharedWeaponData;
        [Title("RIGIDBODY SETTINGS")] [PropertySpace] public WeaponRigidbodyData rigidBodyData;
        [Title("AI SETTINGS")] [PropertyOrder(1000)] [PropertySpace] public WeaponItemAiData aiData;

        [Title("UI - Weapon Selection Menu")]
        public Sprite icon;
        public float iconWidth = 348;
        public float iconHeight = 162;

        private void OnValidate()
        {
            sharedWeaponData = Resources.Load("Weapons/WeaponItemSharedData") as WeaponItemSharedData;
        }
    }
}