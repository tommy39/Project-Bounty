using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace IND.Core.AISystems.Waypoints
{
    public class WaypointNetwork : IND_Mono
    {
        public bool loopWaypointsOnEnd = true;

        public bool alwaysDrawConnection = true;

        [InlineEditor] public List<WaypointController> wayPoints = new List<WaypointController>();

        [Button]
        public void GetAllChildWaypoints()
        {
            wayPoints.Clear();

            WaypointController[] waypoints = GetComponentsInChildren<WaypointController>();
            foreach (WaypointController item in waypoints)
            {
                wayPoints.Add(item);
            }
        }

#if  UNITY_EDITOR	
        private void OnDrawGizmos()
        {
            for (int i = 0; i < wayPoints.Count; i++)
            {
                if (wayPoints[i] == null)
                {
                    wayPoints.Remove(wayPoints[i]);
                    continue;
                }
            }
            Handles.color = Color.black;
            for (int i = 0; i < wayPoints.Count; i++)
            {
                Handles.Label(wayPoints[i].transform.position, (i + 1).ToString());
            }

            if (alwaysDrawConnection)
                OnDrawGizmosSelected();
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < wayPoints.Count; i++)
            {
                if (wayPoints[i] == null)
                {
                    wayPoints.Remove(wayPoints[i]);
                    continue;
                }

                if (i != wayPoints.Count - 1)
                {
                    Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
                }
                else
                {
                    if (loopWaypointsOnEnd)
                    {
                        Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[0].transform.position);
                    }
                }


            }
        }
#endif
    }
}