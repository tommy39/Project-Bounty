using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Sirenix.OdinInspector;
using IND.Core.Weapons;

namespace IND.DevTools.Weapons
{

    public class IND_WeaponEditorTool : OdinEditorWindow
    {
        public static IND_WeaponEditorTool window;

        [InlineEditor] public List<WeaponItem> meleeItems = new List<WeaponItem>();
        [FoldoutGroup("Ranged Weapons")] [InlineEditor] public List<WeaponItem> riflesAndRepeaters = new List<WeaponItem>();
        [FoldoutGroup("Ranged Weapons")] [InlineEditor] public List<WeaponItem> pistolsAndRevolvers = new List<WeaponItem>();
        [FoldoutGroup("Ranged Weapons")] [InlineEditor] public List<WeaponItem> shotguns = new List<WeaponItem>();
        [FoldoutGroup("Ranged Weapons")] [InlineEditor] public List<WeaponItem> bows = new List<WeaponItem>();

        [MenuItem("IND Tools/Weapon Editor Tool", false, 3)]
        private static void OpenInterface()
        {
            window = GetWindow<IND_WeaponEditorTool>();
            window.meleeItems.Clear();
            window.riflesAndRepeaters.Clear();


            WeaponItem[] allWeapons = GetAllInstances<WeaponItem>();
            foreach (WeaponItem item in allWeapons)
            {
                if (item.weaponType == WeaponType.MELEE)
                {
                    window.meleeItems.Add(item);
                }

                if (item.weaponType == WeaponType.RANGED)
                {
                    WeaponItemRanged rangedWeapon = item as WeaponItemRanged;
                    if(rangedWeapon.ammoType == WeaponRangedAmmoType.RIFLE_AMMO)
                    {
                        window.riflesAndRepeaters.Add(rangedWeapon);
                    }


                    if(rangedWeapon.ammoType == WeaponRangedAmmoType.PISTOL_AMMO)
                    {
                        window.pistolsAndRevolvers.Add(rangedWeapon);
                    }


                    if(rangedWeapon.ammoType == WeaponRangedAmmoType.SHOTGUN_SHELLS)
                    {
                        window.shotguns.Add(rangedWeapon);
                    }

                    if(rangedWeapon.ammoType == WeaponRangedAmmoType.ARROWS)
                    {
                        window.bows.Add(rangedWeapon);
                    }

                }
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 800);

        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }
    }
}