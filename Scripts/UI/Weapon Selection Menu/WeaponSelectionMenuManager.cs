using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.DevSettings;
using IND.Core.Weapons;
using IND.Core.Player.Inventory;
using IND.Core.Player;
using UnityEngine.UI;
using IND.Core.Player.Combat;
using IND.Core.Shared;
using TMPro;

namespace IND.Core.UI.WeaponSelectionMenu
{
    public class WeaponSelectionMenuManager : IND_Mono
    {
        [SerializeField] protected GameObject window;
        [InlineEditor] [SerializeField] protected DevGameSettings devSettings;
        [SerializeField] protected GameObject selectableWeaponWindowPrefab;
        [SerializeField] protected Transform listGridParent;
        [SerializeField] protected Button equipSelectedWeaponsButton;
        [HideInInspector] public int amountOfSelectableWeapons;
        [SerializeField] protected PlayerInventoryData inventoryData;
        [SerializeField] protected TextMeshProUGUI selectedWeaponsAmountTxt;

        private List<SelectableWeaponUI> selectableWeaponWindows = new List<SelectableWeaponUI>();
        private List<SelectableWeaponUI> selectedWeapons = new List<SelectableWeaponUI>();
        private PlayerController playerController;
        private MoveToMousePosition moveToMousePosController;

        [SerializeField] protected List<WeaponItem> selectableWeapons = new List<WeaponItem>();

        public override void Init()
        {
            playerController = FindObjectOfType<PlayerController>();
            amountOfSelectableWeapons = inventoryData.maxInventoryWeaponsAmount;
            equipSelectedWeaponsButton.onClick.AddListener(EquipPlayer);
            moveToMousePosController = FindObjectOfType<PlayerAimTargetIdentifier>().GetComponent<MoveToMousePosition>();
            for (int i = 0; i < selectableWeapons.Count; i++)
            {
                GameObject geo = Instantiate(selectableWeaponWindowPrefab, listGridParent);
                SelectableWeaponUI component = geo.GetComponent<SelectableWeaponUI>();
                component.Setup(selectableWeapons[i]);
                selectableWeaponWindows.Add(component);
            }

            if (devSettings.allowWeaponSelectionMenu == true)
            {
                window.SetActive(true);
                playerController.tickPlayer.value = false;
                moveToMousePosController.isActive = false;
            }
            else
            {
                window.SetActive(false);
            }
        }

        public void OnSelectableWeaponSelected(SelectableWeaponUI wpn)
        {
            selectedWeapons.Add(wpn);
            if (selectableWeapons.Count == amountOfSelectableWeapons)
            {
                LockSelectableWeapons();
            }
            UpdateSelectedWeaponsText();
        }

        public void OnSelectableWeaponDeselected(SelectableWeaponUI wpn)
        {
            selectedWeapons.Remove(wpn);
            UpdateSelectedWeaponsText();
        }

        private void LockSelectableWeapons()
        {
            for (int i = 0; i < selectableWeaponWindows.Count; i++)
            {
                if(selectedWeapons.Contains(selectableWeaponWindows[i]))
                {
                    continue;
                }

                selectableWeaponWindows[i].LockSlot();
            }
        }

        private void EquipPlayer()
        {
            playerController.tickPlayer.value = true;
            moveToMousePosController.isActive = true;
            window.SetActive(false);

            PlayerInventoryController inventoryController = FindObjectOfType<PlayerInventoryController>();
            //Destroy Existing Inventory Weapons
            for (int i = 0; i < inventoryController.inventoryWeapons.Count; i++)
            {
                Destroy(inventoryController.inventoryWeapons[i].gameObject);
                inventoryController.inventoryWeapons.RemoveAt(i);
            }

            List<WeaponItem> weapons = new List<WeaponItem>();
            //Add New Inventory Weapons
            for (int i = 0; i < selectedWeapons.Count; i++)
            {
               weapons.Add(selectedWeapons[i].assignedItem);
            }

            if (weapons.Count > 0)
            {
                inventoryController.SpawnInventoryWeapons(weapons);
            }
            else
            {
                inventoryController.SwitchToFists();
            }
        }

        private void UpdateSelectedWeaponsText()
        {
            string finalText = "Selected Weapons - ";
            finalText += selectedWeapons.Count + "/" + inventoryData.maxInventoryWeaponsAmount;
            selectedWeaponsAmountTxt.text = finalText;
        }
    }
}