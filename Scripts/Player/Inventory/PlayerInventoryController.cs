using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Player.Weapons;
using IND.Core.Weapons.Grenades;
using IND.Core.WorldInteractions;
using IND.Core.Characters.Animations;
using IND.Core.UI.Weapons.Reload;
using IND.Core.Characters;
using IND.Core.Player.Combat;
using IND.Core.Characters.Health;

namespace IND.Core.Player.Inventory
{
    public class PlayerInventoryController : IND_Mono
    {
        [PropertySpace] [InlineEditor] public PlayerInventoryData playerInventoryData;
        public List<WeaponItemRuntime> inventoryWeapons = new List<WeaponItemRuntime>();
        [Required] public WeaponItemRuntimeMelee fistsWeaponRuntime;
        [HideInInspector] public PlayerInventoryAmmo inventoryAmmo;
        private int currentWeaponInList = 0;

        public WeaponItem currentWeaponItem;
        public WeaponItemRuntime currentWeaponItemRuntime;

        #region REQUIRED REFERENCES
        private Transform playerRightHand;
        [SerializeField] private Transform playerLeftHand;
        private ScriptableBool isPlayerReloading;
        #endregion

        #region Class References
        [HideInInspector] public InteractionSearcherController searcherController;
        [HideInInspector] public Transform worldInteractionParentGroup;
        [HideInInspector] public PlayerWeaponController weaponController;
        private PlayerReloadController reloadController;
        private HealthControllerPlayer playerHealthController;
        #endregion

        #region Layer Masks
        [HideInInspector] public ScriptableLayerMask playerMeleeWeaponLayerMask;
        [InlineEditor] public ScriptableLayerMask enemyNotificationsMask;
        #endregion

        #region Inputs 
        private ScriptableInputBoolAction toggleWeaponInputAction;
        private ScriptableInputBoolAction throwWeaponInputAction;
        #endregion
        public override void Init()
        {
            weaponController = GetComponent<PlayerWeaponController>();
            playerHealthController = GetComponent<HealthControllerPlayer>();
            reloadController = GetComponent<PlayerReloadController>();

            playerMeleeWeaponLayerMask = Resources.Load("LayerMasks/LayerMask_Player Melee Weapon") as ScriptableLayerMask;
            throwWeaponInputAction = Resources.Load("Input Actions/Input Action Throw Weapon") as ScriptableInputBoolAction;
            toggleWeaponInputAction = Resources.Load("Input Actions/Input Action Toggle Weapon") as ScriptableInputBoolAction;

            isPlayerReloading = Resources.Load("Player/PlayerIsReloading") as ScriptableBool;

            searcherController = GetComponent<InteractionSearcherController>();
            worldInteractionParentGroup = FindObjectOfType<WorldInteractionsManager>().transform;
            playerRightHand = GetComponentInChildren<RightHandIdentifier>().transform;

            inventoryAmmo.rifleAmmo = playerInventoryData.inventoryAmmo.rifleAmmo;
            inventoryAmmo.pistolAmmo = playerInventoryData.inventoryAmmo.pistolAmmo;
            inventoryAmmo.shotgunShells = playerInventoryData.inventoryAmmo.shotgunShells;
            inventoryAmmo.arrows = playerInventoryData.inventoryAmmo.arrows;

            fistsWeaponRuntime.FreshSpawnWeapon();
            fistsWeaponRuntime.PickupWeaponPlayer();
            fistsWeaponRuntime.DisableAttackColliders();

            SpawnInventoryWeapons(playerInventoryData.inventoryWeapons);
        }

        public override void Tick()
        {
            currentWeaponItemRuntime.Tick();
            if (currentWeaponItemRuntime.weaponItemData.weaponType != WeaponType.FISTS)
            {
                if (throwWeaponInputAction.value.value)
                {
                    DropWeaponToGround(true, currentWeaponItemRuntime);
                    WeaponToggle();
                }

                if (toggleWeaponInputAction.value.value)
                {
                    WeaponToggle();
                }
            }
        }
        public void SpawnInventoryWeapons(List<WeaponItem> weapons)
        {    
            if (weapons.Count > 0)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    SpawnFreshWeapon(weapons[i]);
                }
                currentWeaponInList = 1;
                EquipWeaponExistingInInventory(inventoryWeapons[currentWeaponInList - 1]);
                weaponController.OnWeaponEquipped(currentWeaponItemRuntime);
            }
            else
            {
                SwitchToFists();
            }
        }

        void SpawnFreshWeapon(WeaponItem item)
        {
            WeaponItemRanged_DualWielded dualItem = item as WeaponItemRanged_DualWielded;
            if (dualItem != null)
            {
                SpawnDualWieldItem(dualItem);
                return;
            }

            GameObject weaponGeo = Instantiate(item.weaponPrefab, playerRightHand);
            weaponGeo.transform.localPosition = Vector3.zero;
            weaponGeo.transform.localRotation = Quaternion.identity;
            weaponGeo.SetActive(false);

            WeaponItemRuntime runtimeComponent = weaponGeo.GetComponent<WeaponItemRuntime>();
            runtimeComponent.FreshSpawnWeapon();
            runtimeComponent.PickupWeaponPlayer();
            runtimeComponent.Init();
            inventoryWeapons.Add(runtimeComponent);
        }

        private void SpawnDualWieldItem(WeaponItemRanged_DualWielded item)
        {
            //Spawn Right Hand Weapon
            GameObject rightHandGeo = Instantiate(item.weaponPrefab, playerRightHand);
            rightHandGeo.transform.localPosition = Vector3.zero;
            rightHandGeo.transform.localRotation = Quaternion.identity;
            WeaponItemRuntimeRanged rightHandRangedComponent = rightHandGeo.GetComponent<WeaponItemRuntimeRanged>();
            rightHandRangedComponent.FreshSpawnWeapon();
            rightHandRangedComponent.PickupWeaponPlayer();
            rightHandRangedComponent.Init();
            rightHandGeo.SetActive(false);

            //Spawn Left Hand Weapon
            GameObject leftHandGeo = Instantiate(item.weaponPrefab, playerLeftHand);
            leftHandGeo.transform.localPosition = Vector3.zero;
            leftHandGeo.transform.localRotation = Quaternion.identity;
            WeaponItemRuntimeRanged leftHandRangedComponent = leftHandGeo.GetComponent<WeaponItemRuntimeRanged>();
            leftHandRangedComponent.FreshSpawnWeapon();
            leftHandRangedComponent.PickupWeaponPlayer();
            leftHandRangedComponent.Init();
            leftHandRangedComponent.pivot.localPosition = leftHandRangedComponent.rangedData.leftHandPivotPosition;
            leftHandRangedComponent.pivot.localRotation = Quaternion.Euler(leftHandRangedComponent.rangedData.leftHandPivotRotation);
            leftHandGeo.SetActive(false);

            //Setup Dual Wield Component 
            WeaponItemRuntimeRangedDualWielded dualWieldComponent = leftHandGeo.AddComponent<WeaponItemRuntimeRangedDualWielded>();
            WeaponItemRanged_DualWielded dualData = leftHandRangedComponent.rangedData.dualWieldParentData;
            dualWieldComponent.SetupDualWield(dualData, leftHandRangedComponent, rightHandRangedComponent);
            inventoryWeapons.Add(dualWieldComponent);
        }

        public void ChangeCurrentInHandWeapon(WeaponItemRuntime itemRuntime)
        {
            DisableInHandWeapon();

            currentWeaponItemRuntime = itemRuntime;
            currentWeaponItem = itemRuntime.weaponItemData;

            WeaponItemRuntimeRangedDualWielded dualWield = itemRuntime as WeaponItemRuntimeRangedDualWielded;
            if (dualWield == null)
            {
                itemRuntime.gameObject.SetActive(true);
            }
            else
            {
                dualWield.OnWeaponEquipped();
            }
            weaponController.OnWeaponEquipped(currentWeaponItemRuntime);
        }

        void DisableInHandWeapon()
        {
            if (currentWeaponItemRuntime == null)
                return;

            currentWeaponItemRuntime.gameObject.SetActive(false);
        }

        public void EquipNewWeaponToInventory(WeaponItemRuntime itemRuntime)
        {
            bool canBeDualWielded = false;
            if (itemRuntime.weaponItemData.weaponType == WeaponType.RANGED)
            {
                WeaponItemRuntimeRanged rangedItem = itemRuntime as WeaponItemRuntimeRanged;
                if (rangedItem.rangedData.canBeDualWielded == true)
                {
                    //Check Inventory to see if we have the same item type
                    bool hasExistingWeaponInInventory = false;
                    int wpnInList = 0;
                    for (int i = 0; i < inventoryWeapons.Count; i++)
                    {
                        if (inventoryWeapons[i].weaponItemData == itemRuntime.weaponItemData)
                        {
                            hasExistingWeaponInInventory = true;
                            wpnInList = i;
                            break;
                        }
                    }

                    if (hasExistingWeaponInInventory == true)
                    {
                        canBeDualWielded = true;
                        //If Reloading During Pickup Cancel
                        if (isPlayerReloading.value)
                        {
                            reloadController.ReloadCancelled();
                        }
                        //Change Our In Hand Weapon To That If We Have Not Got It (But Don't Execute usual logic)
                        if (currentWeaponItemRuntime != itemRuntime)
                        {

                            if (currentWeaponItemRuntime != null)
                            {
                                currentWeaponItemRuntime.gameObject.SetActive(false);
                            }
                            currentWeaponInList = wpnInList;
                            EquipWeaponExistingInInventory(inventoryWeapons[wpnInList]);
                        }
                        SetupDualWieldItem(itemRuntime);
                    }
                }
            }

            if (canBeDualWielded == false) //If Not Dual Wieldable We can pickup weapons normally
            {
                if (inventoryWeapons.Count == playerInventoryData.maxInventoryWeaponsAmount)
                {
                    DropWeaponToGround(false, currentWeaponItemRuntime);
                }
                else
                {
                    DisableInHandWeapon();
                }
                NewWeaponEquipped(itemRuntime.weaponItemData.weaponType, itemRuntime);
            }
        }
        public void NewWeaponEquipped(WeaponType newWeapon, WeaponItemRuntime itemRuntime)
        {
            itemRuntime.transform.SetParent(playerRightHand);
            itemRuntime.transform.localPosition = Vector3.zero;
            itemRuntime.transform.localRotation = Quaternion.identity;
            itemRuntime.canAttack = true;
            itemRuntime.PickupWeaponPlayer();
            inventoryWeapons.Add(itemRuntime);
            ChangeCurrentInHandWeapon(itemRuntime);
        }

        private void SetupDualWieldItem(WeaponItemRuntime item)
        {
            //Place Weapon In Left Hand
            item.transform.SetParent(playerLeftHand);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            //Get Ranged Component To Setup Left Hand Pivot
            WeaponItemRuntimeRanged leftHandRangedComponent = item as WeaponItemRuntimeRanged;
            leftHandRangedComponent.pivot.localPosition = leftHandRangedComponent.rangedData.leftHandPivotPosition;
            leftHandRangedComponent.pivot.localRotation = Quaternion.Euler(leftHandRangedComponent.rangedData.leftHandPivotRotation);
            leftHandRangedComponent.PickupWeaponPlayer();

            WeaponItemRuntimeRanged rightHandRangedComponent = inventoryWeapons[currentWeaponInList] as WeaponItemRuntimeRanged;


            //Add Dual Wield Component To It
            WeaponItemRuntimeRangedDualWielded dualWieldComponent = item.gameObject.AddComponent<WeaponItemRuntimeRangedDualWielded>();
            WeaponItemRanged_DualWielded dualData = leftHandRangedComponent.rangedData.dualWieldParentData;
            dualWieldComponent.SetupDualWield(dualData, leftHandRangedComponent, rightHandRangedComponent);

            //Swap Inventory To The Dual Wield Component
            inventoryWeapons[currentWeaponInList] = dualWieldComponent;
            currentWeaponItem = dualData;
            currentWeaponItemRuntime = dualWieldComponent;
            weaponController.OnWeaponEquipped(currentWeaponItemRuntime);
        }

        public void DropWeaponToGround(bool throwWeaponForce, WeaponItemRuntime weaponToDrop)
        {
            bool isDualWielded = false;
            if (weaponToDrop.weaponItemData.weaponType == WeaponType.RANGED)
            {
                WeaponItemRuntimeRanged rangedComponent = weaponToDrop as WeaponItemRuntimeRanged;
                if (rangedComponent.isDualWielded == true)
                {
                    isDualWielded = true;
                }
            }

            if (isDualWielded == false)
            {
                if (throwWeaponForce)
                {
                    WorldInteractionEvent_PickupWeaponRuntime pickupWeaponInteraction = WeaponStaticUtils.DropWeaponToGround(weaponToDrop, worldInteractionParentGroup, searcherController.controllerData.worldInteractionPrefab);
                    currentWeaponItemRuntime.OnDropWeaponToGround();
                    WeaponStaticUtils.ThrowWeaponWithForce(pickupWeaponInteraction, weaponToDrop.weaponItemData.throwDistanceForce, transform.forward);
                }
            }
            else
            {
                WeaponItemRuntimeRangedDualWielded dualWieldComponent = weaponToDrop as WeaponItemRuntimeRangedDualWielded;
                dualWieldComponent.DestroyDualWield();
            }

            inventoryWeapons.Remove(weaponToDrop);

            currentWeaponItemRuntime = null;
            currentWeaponItem = null;

            if (playerHealthController.isDead == false)
            {
                WeaponToggle();
            }
        }

        void WeaponToggle()
        {
            currentWeaponInList++;
            if (currentWeaponInList > inventoryWeapons.Count)
            {
                currentWeaponInList = 1;
            }

            if (isPlayerReloading.value)
            {
                reloadController.ReloadCancelled();
            }


            if (inventoryWeapons.Count == 0)
            {
                SwitchToFists();
                return;
            }
            if (currentWeaponItemRuntime != null)
            {
                currentWeaponItemRuntime.gameObject.SetActive(false);
            }

            EquipWeaponExistingInInventory(inventoryWeapons[currentWeaponInList - 1]);
        }

        void EquipWeaponExistingInInventory(WeaponItemRuntime weapon)
        {
            currentWeaponItemRuntime = weapon;
            currentWeaponItem = weapon.weaponItemData;
            WeaponItemRuntimeRangedDualWielded dualWield = weapon as WeaponItemRuntimeRangedDualWielded;
            if (dualWield == null)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                dualWield.OnWeaponEquipped();
            }
            weaponController.OnWeaponEquipped(currentWeaponItemRuntime);
        }

        public void SwitchToFists()
        {
            currentWeaponItemRuntime = fistsWeaponRuntime;
            weaponController.OnWeaponEquipped(currentWeaponItemRuntime);
        }
    }
}