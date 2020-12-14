using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    [CreateAssetMenu(fileName = "Weapon_Ranged_Name", menuName = "IND/Core/Weapons/WeaponItemRanged")]
    public class WeaponItemRanged : WeaponItem
    {
        [FoldoutGroup("WEAPON SETUP")] public WeaponRangedType rangedType;
        [FoldoutGroup("WEAPON SETUP")] public WeaponRangedFireRateMode fireRateMode;
        [FoldoutGroup("WEAPON SETUP")] public WeaponBulletType shootType;
        [FoldoutGroup("WEAPON SETUP")] public WeaponRangedAmmoType ammoType;
        [FoldoutGroup("WEAPON SETUP")] public bool canBeDualWielded = false;
        [FoldoutGroup("WEAPON SETUP")] [ShowIf("canBeDualWielded")] public WeaponItemRanged_DualWielded dualWieldParentData;
        [FoldoutGroup("WEAPON SETUP")] [ShowIf("canBeDualWielded")] public Vector3 leftHandPivotPosition;
        [FoldoutGroup("WEAPON SETUP")] [ShowIf("canBeDualWielded")] public Vector3 leftHandPivotRotation;
        [FoldoutGroup("WEAPON SETUP")] public GameObject bulletToFirePrefab;
        [FoldoutGroup("WEAPON SETUP")] [Required] public GameObject bulletCasingPrefab;

        [PropertyRange(1, 100)] [FoldoutGroup("Attack Data")] public float bulletMovementSpeed = 50f;
        [FoldoutGroup("Attack Data")] public int maxMagazineAmmoAmount = 5;
        [PropertyRange(1, 100)] [FoldoutGroup("Attack Data")] public float gunshotSoundNotificationDistance = 5f;

        [PropertyRange(1, 10)]
        [FoldoutGroup("Attack Data")]
        [HideIf("shootType", WeaponBulletType.SINGLE_BULLET)]
        [HideIf("shootType", WeaponBulletType.SINGLE_BOLT)]
        public int spreadAmount = 3;

        [PropertyRange(0, 25)]
        [FoldoutGroup("Attack Data")]
        [HideIf("shootType", WeaponBulletType.SINGLE_BULLET)]
        [HideIf("shootType", WeaponBulletType.SPREAD_BULLET)]
        public float arrowBodyHitOffset = 5;


        [PropertyRange(1, 180)]
        [FoldoutGroup("Attack Data")]
        [HideIf("shootType", WeaponBulletType.SINGLE_BULLET)]
        [HideIf("shootType", WeaponBulletType.SINGLE_BOLT)]
        public int spreadMaxRange = 3;

        [FoldoutGroup("Attack Data/Reload Data")] public float animReloadLengthSeconds = 2f;
        [FoldoutGroup("Attack Data/Reload Data")] [PropertyRange(0, 1)] public float positiveReloadAreaWidth;
        [FoldoutGroup("Attack Data/Reload Data")] [PropertyRange(0, 1)] public float positiveReloadAreaCentrePosition;

        [Title("Audio")]
        public AudioClip fireShotSound;
        public AudioClip reloadSound;
        public AudioClip magazineOutOfAmmoSound;
    }
}