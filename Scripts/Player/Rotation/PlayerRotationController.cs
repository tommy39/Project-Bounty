using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Cameras;

namespace IND.Core.Player.Rotation
{
    public class PlayerRotationController : IND_Mono
    {
        [Required] [InlineEditor] public ScriptableFloat playerRotationSpeed;

        private ScriptableFloat deltaTimer; 
        private ScriptableVector3 currentMousePos;

        private Camera gameCam;
        public override void Init()
        {
            gameCam = FindObjectOfType<CamControllerGameplay>().GetComponent<Camera>();
            deltaTimer = GetResource.DeltaTimer();
            currentMousePos = Resources.Load("Inputs/Mouse Inputs/Input_MousePosWorldSpace") as ScriptableVector3;
        }

        public override void Tick()
        {
            Ray ray = gameCam.ScreenPointToRay(currentMousePos.value);
            Plane ground = new Plane(Vector3.down, Vector3.zero);
            float rayDistance;

            if (ground.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 dir = point - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, playerRotationSpeed.value * deltaTimer.value);
            }
        }

        public override void FixedTick()
        {

        }
    }
}