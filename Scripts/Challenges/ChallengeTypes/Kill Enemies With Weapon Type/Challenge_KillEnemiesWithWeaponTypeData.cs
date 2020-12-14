using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;

namespace IND.Core.Challenges.KillEnemiesWithWeaponType
{
    [CreateAssetMenu(fileName = "Kill Enemies With WeaponType", menuName = "IND/Core/Challenges/Kill Enemies With WeaponType")]
    public class Challenge_KillEnemiesWithWeaponTypeData : ChallengeData
    {
        public bool killAllEnemiesInMission = false;
        [HideIf("killAllEnemiesInMission")] public int amountOfEnemiesToKill = 0;

        public TypeOfWeaponToTrack typeOfWeaponToTrack;

        [ShowIf("typeOfWeaponToTrack", TypeOfWeaponToTrack.weaponType)] public WeaponType weaponType;
        [ShowIf("typeOfWeaponToTrack", TypeOfWeaponToTrack.specificWeapon)] public WeaponItem specificWeapon;

        public override void SetupChallenegeRuntime(ChallengeManager challengeManager)
        {
            Challenge_KillEnemiesWithWeaponTypeRuntime runtime = challengeManager.gameObject.AddComponent<Challenge_KillEnemiesWithWeaponTypeRuntime>();
            runtime.challengeData = this;
            challengeManager.activeChalleneges.Add(runtime);
            runtime.Init();
        }

    }

    [System.Serializable]
    public enum TypeOfWeaponToTrack
    {
        weaponType, weaponGroup, specificWeapon
    }

}