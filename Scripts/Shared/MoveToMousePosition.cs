using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Cameras;
using IND.Core.Player;

namespace IND.Core.Shared
{
    public class MoveToMousePosition : IND_Mono
    {
        public Transform yHeightReference;
        public float yHeightModifier = 1f;

        [InlineEditor] public ScriptableLayerMask aimTargetsLayerMask;
        public bool isActive = true;

        private Camera gameCamera;
        private ScriptableVector3 currentMousePos;

        Ray castPoint;
        RaycastHit hit;
        public override void Init()
        {
            yHeightReference = FindObjectOfType<PlayerController>().transform;
            gameCamera = FindObjectOfType<CamControllerGameplay>().GetComponent<Camera>();
        }

        public override void Tick()
        {
            if (isActive == false)
                return;

            castPoint = gameCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out hit, 100f, aimTargetsLayerMask.value))
            {
                transform.position = hit.point;
                transform.position = new Vector3(transform.position.x, yHeightReference.position.y + yHeightModifier, transform.position.z);
            }

        }

    }
}