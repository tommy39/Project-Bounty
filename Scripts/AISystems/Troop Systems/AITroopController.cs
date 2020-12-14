using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Animations;
using UnityEngine.AI;
using UnityEditor;
using IND.Core.Weapons;
using IND.Core.AISystems.Inventory;
using IND.Core.Characters;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using UnityEditor.Experimental.SceneManagement;

namespace IND.Core.AISystems.TroopSystems
{
    public class AITroopController : IND_Mono
    {
        private AIController aiController;
        private NavMeshAgent agent;

        public Transform characterMeshSpawnParent;

        public AITroopType troopType;
        private AITroopType createdTroopType;

        public GameObject createdCharacterMesh;
        [InlineEditor] public AITroopData troopData;

        public bool useDefaultWeapon = true;
        [HideIf("useDefaultWeapon")] public WeaponItem weaponToSpawnWith;

        public override void Init()
        {
            aiController = GetComponent<AIController>();
            UpdateNavMeshAgentSettings();
            aiController.inventoryController.currentInHandWeaponData = troopData.defaultWeaponToSpawnWith;
        }
        private void UpdateNavMeshAgentSettings()
        {
            agent = GetComponent<NavMeshAgent>();

            agent.speed = troopData.navMeshSettings.speed;
            agent.angularSpeed = troopData.navMeshSettings.angularSpeed;
            agent.acceleration = troopData.navMeshSettings.acceleration;
            agent.stoppingDistance = troopData.navMeshSettings.stoppingDistance;
            agent.autoBraking = troopData.navMeshSettings.autoBraking;

            agent.radius = troopData.navMeshSettings.radius;
            agent.height = troopData.navMeshSettings.height;
            agent.obstacleAvoidanceType = troopData.navMeshSettings.avoidanceType;
            agent.avoidancePriority = troopData.navMeshSettings.avoidancePriority;
        }

        [Button]
        private void CopyNavMeshAgentDataToScriptableObject()
        {
            troopData.navMeshSettings.speed = agent.speed;
            troopData.navMeshSettings.angularSpeed = agent.angularSpeed;
            troopData.navMeshSettings.acceleration = agent.acceleration;
            troopData.navMeshSettings.stoppingDistance = agent.stoppingDistance;
            troopData.navMeshSettings.autoBraking = agent.autoBraking;

            troopData.navMeshSettings.radius = agent.radius;
            troopData.navMeshSettings.height = agent.height;
            troopData.navMeshSettings.avoidanceType = agent.obstacleAvoidanceType;
            troopData.navMeshSettings.avoidancePriority = agent.avoidancePriority;
        }

#if UNITY_EDITOR
        void SpawnCharacterMesh()
        {
            bool hasExistingCharacter = CheckIfHasExistingCharacterMesh();
            if (hasExistingCharacter == true)
            {
                bool isCorrectCharacterMesh = CheckIfHasCorrectCharacterMesh();
                if (isCorrectCharacterMesh == true)
                {
                    return;
                }
                else
                {
                    DestoryExistingCharacterMesh();
                }
            }


            LimbsController[] limbControllers = GetComponentsInChildren<LimbsController>();
            foreach (LimbsController item in limbControllers)
            {
                DestroyImmediate(item.gameObject);
            }
            if (createdTroopType != troopType)
            {
                if (createdCharacterMesh != null)
                {
                    DestroyImmediate(createdCharacterMesh);
                }

                createdTroopType = troopType;
            }


            if (createdCharacterMesh == null)
            {
                createdCharacterMesh = PrefabUtility.InstantiatePrefab(troopData.characterPrefab) as GameObject;
                createdCharacterMesh.transform.SetParent(characterMeshSpawnParent);
                createdCharacterMesh.transform.localPosition = Vector3.zero;
                createdCharacterMesh.transform.localRotation = Quaternion.identity;
            }
        }

        void DestoryExistingCharacterMesh()
        {
            HitboxController hitboxController = GetComponentInChildren<HitboxController>();
            DestroyImmediate(hitboxController.gameObject);
        }

        bool CheckIfHasExistingCharacterMesh()
        {
            if (GetComponentInChildren<LimbsController>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckIfHasCorrectCharacterMesh()
        {
            HitboxController controller = GetComponentInChildren<HitboxController>();
            string currentCharacterGameObjectName = controller.gameObject.name;
            if (currentCharacterGameObjectName == troopData.characterPrefab.gameObject.name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Button]
        void ForceDestroyCharacterMesh()
        {
            LimbsController[] limbControllers = GetComponentsInChildren<LimbsController>();
            foreach (LimbsController item in limbControllers)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        void SpawnWeapon()
        {
            WeaponItem weaponToSpawn = null;
            if (useDefaultWeapon == true)
            {
                weaponToSpawn = troopData.defaultWeaponToSpawnWith;
            }
            else
            {
                weaponToSpawn = weaponToSpawnWith;
            }

            bool HasExistingWeapon = CheckForExistingWeapon();
            if (HasExistingWeapon == true)
            {
                bool isSameType = CheckIfExistingWeaponIsSameType(weaponToSpawn);
                if (isSameType == true)
                {
                    return;
                }
                else
                {
                    WeaponItemRuntime currentWeapon = GetComponentInChildren<WeaponItemRuntime>();
                    if (currentWeapon != null)
                    {
                        if (currentWeapon.weaponItemData != weaponToSpawn)
                        {
                            DestroyImmediate(currentWeapon.gameObject);
                            currentWeapon = null;
                        }
                    }
                }

            }



            GameObject createdGEO = PrefabUtility.InstantiatePrefab(weaponToSpawn.weaponPrefab) as GameObject;
            RightHandIdentifier rightHand = GetComponentInChildren<RightHandIdentifier>();
            //               Debug.Log(rightHand);
            //               Debug.Log(createdGEO);
            createdGEO.transform.parent = rightHand.transform;
            createdGEO.transform.localPosition = Vector3.zero;
            createdGEO.transform.localRotation = Quaternion.identity;
        }


        bool CheckForExistingWeapon()
        {
            if (GetComponentInChildren<WeaponItemRuntime>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckIfExistingWeaponIsSameType(WeaponItem weapon)
        {
            WeaponItemRuntime runtimeItem = GetComponentInChildren<WeaponItemRuntime>();
            if (runtimeItem.weaponItemData.weaponName == weapon.weaponName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [ExecuteInEditMode]
        private void GetTroopData()
        {
            switch (troopType)
            {
                case AITroopType.MeleeBandit:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_MeleeBandit") as AITroopData;
                    break;
                case AITroopType.Gunslinger:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Gunslinger") as AITroopData;
                    break;
                case AITroopType.Flanker:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Flanker") as AITroopData;
                    break;
                case AITroopType.Scout:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Scout") as AITroopData;
                    break;
                case AITroopType.Tank:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Tank") as AITroopData;
                    break;
                case AITroopType.Boss_01:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Boss_01") as AITroopData;
                    break;
                case AITroopType.Boss_02:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Boss_02") as AITroopData;
                    break;
                case AITroopType.Boss_03:
                    troopData = Resources.Load("AI/Troop Systems/AI_Troop_Data_Boss_03") as AITroopData;
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;

            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();

            if (stage != null)
                return;

            GetTroopData();
            SpawnCharacterMesh();
            SpawnWeapon();
        }

#endif
    }
}