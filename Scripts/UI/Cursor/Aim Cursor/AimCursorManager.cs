using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.UI.Cursors.AimCursor
{
    public class AimCursorManager : IND_Mono
    {

        public RectTransform aimTransform;

        [InlineEditor] public ScriptableBool enableMouseCursor;
        public override void Init()
        {
            if (enableMouseCursor.value == false)
            {
                DisableMouseCursor(false);
            }
            else
            {
                EnableMouseCursor(false);
            }
            EnableAimCursor();
        }

        public void EnableAimCursor()
        {
            aimTransform.gameObject.SetActive(true);
        }

        public void DisableAimCursor()
        {
            aimTransform.gameObject.SetActive(false);
        }

        public void DisableMouseCursor(bool disableAimCursor)
        {
            Cursor.visible = false;

            if (disableAimCursor == false)
            {
                EnableAimCursor();
            }
            else
            {
                DisableAimCursor();
            }
        }

        public void EnableMouseCursor(bool enableAimCursor)
        {
            Cursor.visible = true;

            if (enableAimCursor == true)
            {
                EnableAimCursor();
            }
            else
            {
                DisableAimCursor();
            }
        }

        public override void Tick()
        {
            aimTransform.position = Input.mousePosition;
        }

        public static AimCursorManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}