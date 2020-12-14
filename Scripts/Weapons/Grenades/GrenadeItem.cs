using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons.Grenades
{
    [CreateAssetMenu(fileName = "Weapon_Grenade_Name", menuName = "IND/Core/Weapons/Grenades/GrenadeItem")]
    public class GrenadeItem : ScriptableObject
    {
        public string grenadeName;

        [Required] public GameObject explosionEffect;
        [InlineEditor] public ScriptableLayerMask hitablesLayerMask;

        [PropertyRange(0, 10)] public float timeBeforeExploding = 1f;
        [PropertyRange(0, 10)] public float destroyFinalTimer = 5f;

        [PropertyRange(0, 10)] public float explosionRadius;
        [PropertyRange(0, 200)] public int damage;
        [PropertyRange(0, 1000)] public float throwForce = 1000f;

        public WeaponRigidbodyData rigidBodyData;

        [PreviewField(200, ObjectFieldAlignment.Left)]
        public GameObject weaponPrefab;


    }
}