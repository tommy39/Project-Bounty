using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.AI;
using IND.Core.Shared.Statics;

namespace IND.Core.AISystems.Movement
{
    public class AIMovementController : IND_Mono
    {
        [HideInInspector] public NavMeshAgent agent;

        public bool isMoving;
        public override void Init()
        {
            agent = GetComponent<NavMeshAgent>();

            isMoving = false;
        }

        public void SetAgentDestination(Vector3 targetDestination)
        {
       //     Vector3 newDestination = GetNearestNavMeshPoint.GetClosestNavMeshPoint(targetDestination);
            agent.SetDestination(targetDestination);
            if (isMoving == false)
            {
                isMoving = true;
                agent.isStopped = false;
            }
        }

        public void StopAgent()
        {
            if (isMoving)
            {
                isMoving = false;
                agent.isStopped = true;
            }
        }

        public void OnDeath()
        {
            agent.enabled = false;
        }
    }
}