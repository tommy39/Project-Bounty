using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Inventory;

namespace IND.Core.WorldInteractions
{
    public class InteractionSearcherController : IND_Mono
    {
        [HideInInspector] public PlayerInventoryController inventoryController;

        [InlineEditor] public WorldInteractionController nearestAvailableInteraction;
        [InlineEditor] public InteractionSearcherControllerData controllerData;

        public bool drawDebugSphere;

        private int currentSearchFrame = 0;
        float currentShortestDistance;
        float distanceBetweenObjectsInLoop;

        private Collider[] hitColliders;

        private ScriptableInputBoolAction activateInteractionInput;
        public override void Init()
        {
            inventoryController = GetComponent<PlayerInventoryController>();
            currentShortestDistance = controllerData.maxSearchDistance * 2;
            activateInteractionInput = Resources.Load("Input Actions/Input Action Interaction") as ScriptableInputBoolAction;
        }

        public override void Tick()
        {
            if (currentSearchFrame < controllerData.searchFrameRate)
            {
                currentSearchFrame++;
                return;
            }

            SearchForNearbyWorldInteractions();

            if (nearestAvailableInteraction != null)
            {
                HandleInteractionInput();
            }
        }

        void HandleInteractionInput()
        {
            if (activateInteractionInput.value.value == false)
                return;

            nearestAvailableInteraction.ExecuteInteraction(this);
        }
        void SearchForNearbyWorldInteractions()
        {
            hitColliders = Physics.OverlapSphere(transform.position, controllerData.maxSearchDistance, controllerData.worldInteractionsLayerMask.value);
            CheckIfNearestInteractionIsStillInRange();
            GetClosestInteraction();
        }

        void CheckIfNearestInteractionIsStillInRange()
        {
            if (nearestAvailableInteraction == null)
                return;

            distanceBetweenObjectsInLoop = Vector3.Distance(transform.position, nearestAvailableInteraction.transform.position);

            if(distanceBetweenObjectsInLoop > currentShortestDistance)
            {
                LeaveClosestInteractionRange();
            }
        }

        void GetClosestInteraction()
        {
            if (hitColliders.Length == 0) //No Available Interactions
            {
                return;
            }
            currentShortestDistance = controllerData.maxSearchDistance;

            for (int i = 0; i < hitColliders.Length; i++)
            {
                distanceBetweenObjectsInLoop = Vector3.Distance(transform.position, hitColliders[i].transform.position);

                if(distanceBetweenObjectsInLoop < currentShortestDistance) //Is Shorter than the current distance
                {
                    if(nearestAvailableInteraction != null)
                    {
                        if(nearestAvailableInteraction.transform != hitColliders[i].transform)
                        {
                            LeaveClosestInteractionRange();
                        }
                    }
                    nearestAvailableInteraction = hitColliders[i].GetComponent<WorldInteractionController>();
                    currentShortestDistance = distanceBetweenObjectsInLoop;
                }
            }
        }

        void LeaveClosestInteractionRange()
        {
            nearestAvailableInteraction = null;
        }

        private void OnDrawGizmosSelected()
        {
            if (drawDebugSphere == false)
                return;

            Gizmos.color = Color.white;
            //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
            Gizmos.DrawWireSphere(transform.position, controllerData.maxSearchDistance);
        }
    }
}