using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems.Waypoints
{
    public enum WaypointOnReachTaskType
    {
        MOVE_TO_NEXT_WAYPOINT, STOP_AND_WAIT, STOP_LOOK_AT_TARGETS
    }
}