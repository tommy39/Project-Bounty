using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;
using System;
using IND.Core.Weapons;
using IND.Core.Player.Weapons;
using IND.Core.Player.Inventory;
using IND.Core.Player.Combat;
using IND.Core.UI.Challenges;
using IND.Core.UI.MissionCompleteMenu;

namespace IND.Core.Challenges.KillEnemiesWithWeaponType
{
    public class Challenge_KillEnemiesWithWeaponTypeRuntime : ChallengeRuntime
    {
        private Challenge_KillEnemiesWithWeaponTypeData killEnemiesWithWeaponData;

        [SerializeField] private int enemiesLeftToKill;
        private int totalEnemiesToKill;
        private PlayerWeaponController weaponController;

        public override void Init()
        {
            AIManager aiManager = FindObjectOfType<AIManager>();
            aiManager.OnEnemyKilled += EnemyHasBeenKilled;

            killEnemiesWithWeaponData = challengeData as Challenge_KillEnemiesWithWeaponTypeData;
            weaponController = FindObjectOfType<PlayerWeaponController>();
           

            if (killEnemiesWithWeaponData.killAllEnemiesInMission == true)
            {
                enemiesLeftToKill = aiManager.activeChildAI.Count;
            }
            else
            {
                enemiesLeftToKill = killEnemiesWithWeaponData.amountOfEnemiesToKill;
            }

            totalEnemiesToKill = enemiesLeftToKill;
        }

        public override void Tick()
        {

        }

        private void EnemyHasBeenKilled(object sender, EventArgs e)
        {
            bool isUsingNeededWeapon = false;

            switch (killEnemiesWithWeaponData.typeOfWeaponToTrack)
            {
                case TypeOfWeaponToTrack.weaponType:
                    isUsingNeededWeapon = IsUsingRightWeaponType();
                    break;
                case TypeOfWeaponToTrack.weaponGroup:
                    break;
                case TypeOfWeaponToTrack.specificWeapon:
                    isUsingNeededWeapon = IsUsingSpecificWeapon();
                    break;
            }

            if (isUsingNeededWeapon == false)
                return;

            enemiesLeftToKill--;
            CheckCompletionStateAtRuntime();
        }

        public override void CheckCompletionStateAtRuntime()
        {
            if(enemiesLeftToKill <= 0)
            {
                ChallenegeCompleted();

                FindObjectOfType<UI_MissionCompleteMenuManager>().OnMissionComplete();
            }
        }

        private bool IsUsingRightWeaponType()
        {
            if(weaponController.currentWeaponType == killEnemiesWithWeaponData.weaponType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsUsingSpecificWeapon()
        {
            if(weaponController.currentRuntimeWeapon.weaponItemData == killEnemiesWithWeaponData.specificWeapon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void UpdateUIInformation(UI_ChallengeItem challengeItem)
        {
            completionTextFirstPart = (totalEnemiesToKill - enemiesLeftToKill).ToString();
            completionTextLastPart = totalEnemiesToKill.ToString();
            base.UpdateUIInformation(challengeItem);
        }
    }
}