using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using IND.Core.Weapons;

namespace IND.Core.UI.WeaponSelectionMenu
{
    public class SelectableWeaponUI : MonoBehaviour
    {
        [SerializeField] protected Image img;
        [SerializeField] protected Button btn;
        [SerializeField] protected GameObject lockPanel;
        [SerializeField] protected GameObject slotSelectedPanel;
        [SerializeField] protected TextMeshProUGUI weaponNameTxt;
        public WeaponItem assignedItem;
        private bool isLocked = false;
        private bool isSelected = false;

        private WeaponSelectionMenuManager menuManager;

        public void Setup(WeaponItem wpn)
        {
            assignedItem = wpn;
            img.sprite = wpn.icon;
            btn.onClick.AddListener(ToggleSlotSelection);
            lockPanel.SetActive(false);
            slotSelectedPanel.SetActive(false);
            weaponNameTxt.text = wpn.weaponName;
            menuManager = FindObjectOfType<WeaponSelectionMenuManager>();
        }

        public void LockSlot()
        {
            isLocked = true;
            lockPanel.SetActive(true);
        }

        public void UnLockSlot()
        {
            isLocked = false;
            lockPanel.SetActive(false);
        }

        private void ToggleSlotSelection()
        {
            if(isSelected == true)
            {
                UnMarkSelectedSlot();
            }
            else
            {
                MarkSlotAsSelected();
            }
        }

        public void MarkSlotAsSelected()
        {
            if (isLocked == true)
                return;

            if (isSelected == true)
                return;

            isSelected = true;
            slotSelectedPanel.SetActive(true);
            menuManager.OnSelectableWeaponSelected(this);
        }

        public void UnMarkSelectedSlot()
        {
            if (isSelected == false)
                return;

            isSelected = false;
            slotSelectedPanel.SetActive(false);
            menuManager.OnSelectableWeaponDeselected(this);
        }
    }
}