using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Player.Combat;
using IND.Core.Player.Weapons;
using IND.Core.UI.Cursors.AimCursor;

namespace IND.Core.UI.Weapons.Reload
{
    public class UI_ReloadProgressBar : IND_Mono
    {
        public GameObject parentProgressBar;
        public Transform uiToMove;
        [InlineEditor] public Image progressBar;

        public float reloadTickRate = 0.01f;

        private bool hasActiveReload = false;
        private float reloadSpeed;
        private float currentProgress;
        private float reloadDuration = 2f;
        private float reloadTickIncrease;

        private PlayerReloadController playerReloadController;
        private AimCursorManager aimCursorManager;
        public override void Init()
        {
            playerReloadController = FindObjectOfType<PlayerReloadController>();
            aimCursorManager = FindObjectOfType<AimCursorManager>();

            progressBar.fillAmount = 0;
            currentProgress = 0f;
            parentProgressBar.SetActive(false);
        }

        public override void Tick()
        {
            if(hasActiveReload == true)
            {
                progressBar.fillAmount = currentProgress;
                MoveBarToMousePos();
            }
        }

        void MoveBarToMousePos()
        {
            uiToMove.position = Input.mousePosition;
        }

        public void ActivateNewReload(WeaponItemRanged rangedWeapon, float targetReloadSpeed)
        {
            hasActiveReload = true;
            parentProgressBar.SetActive(true);
            reloadSpeed = targetReloadSpeed;
            currentProgress = 0;
            reloadDuration = rangedWeapon.animReloadLengthSeconds;
            reloadTickIncrease = reloadTickRate;
            aimCursorManager.DisableAimCursor();
            StartCoroutine("TickReload");
        }
  
        private IEnumerator TickReload()
        {
            while (currentProgress < 1)
            {
                yield return new WaitForSeconds(reloadTickRate);
                currentProgress += reloadTickIncrease;

                if (currentProgress >= 1)
                {
                    ReloadCompleted();
                    StopCoroutine("TickReload");
                }
            }
        }

        private void ReloadCompleted()
        {
            hasActiveReload = false;
            parentProgressBar.SetActive(false);
            aimCursorManager.EnableAimCursor();
            playerReloadController.ReloadCompleted();
        }

        public void OnReloadCancelled()
        {
            hasActiveReload = false;
            StopCoroutine("TickReload");
            aimCursorManager.EnableAimCursor();
            parentProgressBar.SetActive(false);
        }
    }
}