using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;
using IND.Core.AISystems.FieldOfView;

namespace IND.Core.AISystems
{
    public class AIAlertController : IND_Mono
    {
        private AISearchAlertController searchAlertController;
        private FieldOfViewController fieldOfViewController;

        [InlineEditor] public ScriptableFloat playerSpottedAlertDistance;
        public bool spottedAlertSent = false;

        private ScriptableLayerMask searchNotificationMask;
        [InlineEditor] public ScriptableBool drawAiAlertRadiusSphere;

        private Collider[] collisionsInSphereCast;
        private List<AISearchAlertController> nearbyAI;
        public override void Init()
        {
            searchNotificationMask = Resources.Load("LayerMasks/LayerMask_Enemy Notifications") as ScriptableLayerMask;
            searchAlertController = GetComponent<AISearchAlertController>();
            fieldOfViewController = GetComponentInChildren<FieldOfViewController>();
        }
        public void SendPlayerSpottedNotification()
        {
            if (spottedAlertSent == false)
            {
                collisionsInSphereCast = Physics.OverlapSphere(transform.position, playerSpottedAlertDistance.value, searchNotificationMask.value);

                foreach (Collider item in collisionsInSphereCast)
                {
                    if(fieldOfViewController.playerTarget == null)
                    {
                        break;
                    }

                    item.GetComponent<AINotificationHandler>().searchAlertController.SearchAreaNotifcationRecieved(fieldOfViewController.playerTarget.transform.position);
                }

                spottedAlertSent = true;
            }
        }

        public void OnDrawGizmos()
        {
            if (drawAiAlertRadiusSphere.value)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawWireSphere(transform.position, playerSpottedAlertDistance.value);
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, playerSpottedAlertDistance.value);
        }
    }
}