using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.LookAtTargetsNetwork;

namespace IND.Core.AISystems.Waypoints
{
    public class WaypointController : IND_Mono
    {
        public WaypointOnReachTaskType onReachTaskType;
        [ShowIf("onReachTaskType", WaypointOnReachTaskType.STOP_AND_WAIT)] public float stopAndWaitTime = 2f;
        [ShowIf("onReachTaskType", WaypointOnReachTaskType.STOP_LOOK_AT_TARGETS)] [InlineEditor] public List<LookAtTargetItem> stopAndLookTargets = new List<LookAtTargetItem>();

        [ShowIf("onReachTaskType", WaypointOnReachTaskType.STOP_LOOK_AT_TARGETS)]
        [Button]
        void GetAllChildLookAtTargets()
        {
            stopAndLookTargets.Clear();
            LookAtTargetItem[] targets = GetComponentsInChildren<LookAtTargetItem>();
            foreach (LookAtTargetItem item in targets)
            {
                stopAndLookTargets.Add(item);
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            switch (onReachTaskType)
            {
                case WaypointOnReachTaskType.MOVE_TO_NEXT_WAYPOINT:
                    break;
                case WaypointOnReachTaskType.STOP_AND_WAIT:
                    break;
                case WaypointOnReachTaskType.STOP_LOOK_AT_TARGETS:
                    Gizmos.color = Color.red;

                    for (int i = 0; i < stopAndLookTargets.Count; i++)
                    {
                        if (i != stopAndLookTargets.Count)
                        {
                            Gizmos.DrawLine(stopAndLookTargets[i].transform.position, transform.position);
                        }

                    }
                    break;
            }

            WaypointNetwork network = GetComponentInParent<WaypointNetwork>();
            network.OnDrawGizmosSelected();

            network.GetAllChildWaypoints();

        }
#endif
    }
}