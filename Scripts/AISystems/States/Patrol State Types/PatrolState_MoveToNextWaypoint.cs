using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems.States.Patrol
{
    [System.Serializable]
    public class PatrolState_MoveToNextWaypoint
    {
        AIStateController stateController;
        AIState_Patrol patrolState;
        public void Init(AIStateController stateCon, AIState_Patrol patrolStates)
        {
            stateController = stateCon;
            patrolState = patrolStates;
        }

        public void OnWaypointReach()
        {
            stateController.patrolState.GetNextWaypointInList();
        }
    }
}