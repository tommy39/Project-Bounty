using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using IND.Core.AISystems;

namespace IND.Core.AISystems.States
{
    public class AIStateController : IND_Mono
    {
        public AIStateType startState;
        public AIStateType currentState;

        [Title("Current State Vars")]
        [ShowIf("currentState", AIStateType.COMBAT)] public AIState_Combat combatState;
        [ShowIf("currentState", AIStateType.IDLE)] public AIState_Idle idleState;
         public AIState_Search searchState;
        [ShowIf("currentState", AIStateType.PATROL)] public AIState_Patrol patrolState;
        private AIKnockDownController knockdownController;
        public override void Init()
        {
            knockdownController = GetComponent<AIKnockDownController>();
            knockdownController.Init();

            currentState = startState;

            combatState.Init(this);

            switch (currentState)
            {
                case AIStateType.IDLE:
                    idleState.Init(this);
                    break;
                case AIStateType.PATROL:
                    patrolState.Init(this);
                    break;
                case AIStateType.COMBAT:
                    break;
                case AIStateType.DUMMY:
                    break;
                case AIStateType.SEARCH:
                    break;
                case AIStateType.KNOCKEDDOWN:
                    break;
            }

            searchState.Init(this);
        }

        public override void Tick()
        {
            TickCurrentState();
        }

        void TickCurrentState()
        {
            switch (currentState)
            {
                case AIStateType.IDLE:
                    idleState.Tick();
                    break;
                case AIStateType.PATROL:
                    patrolState.Tick();
                    break;
                case AIStateType.COMBAT:
                    combatState.Tick();
                    break;
                case AIStateType.DUMMY:                    
                    break;
                case AIStateType.SEARCH:
                    searchState.Tick();
                    break;
                case AIStateType.KNOCKEDDOWN:
                    knockdownController.Tick();
                    break;
            }
        }

        public void ChangeState(AIStateType targetState)
        {
            switch (currentState)
            {
                case AIStateType.IDLE:
                    idleState.OnStateExit();
                    break;
                case AIStateType.PATROL:
                    patrolState.OnStateExit();
                    break;
                case AIStateType.COMBAT:
                    combatState.OnStateExit();
                    break;
                case AIStateType.DUMMY:
                    break;
                case AIStateType.SEARCH:
                    searchState.OnStateExit();
                    break;
            }

            currentState = targetState;

            switch (currentState)
            {
                case AIStateType.IDLE:
                    idleState.OnStateEnter();
                    break;
                case AIStateType.PATROL:
                    patrolState.OnStateEnter();
                    break;
                case AIStateType.COMBAT:
                    combatState.OnStateEnter();
                    break;
                case AIStateType.DUMMY:
                    break;
                case AIStateType.SEARCH:
                    searchState.OnStateEnter();
                    break;
            }
        }
#if Unity_Editor
        private void OnDrawGizmos()
        {
            Handles.color = Color.black;
            Handles.Label(transform.position + new Vector3(0, 2.2f, 0), currentState.ToString());

            if (Application.isPlaying)
                return;

            currentState = startState;
        }
#endif
        
    }
}