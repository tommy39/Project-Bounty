using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems.Waypoints;

namespace IND.Core.AISystems.States
{
    public class AIPatrolStateController : IND_Mono
    {
        [InlineEditor] [Required] public WaypointNetwork waypointNetwork;
        [PropertyTooltip("Value is minused by 1, so use 1 for the first in list")]
        public int numberToStartAtInList;
        public override void Init()
        {

        }

        public override void Tick()
        {

        }

        public override void FixedTick()
        {

        }
    }
}