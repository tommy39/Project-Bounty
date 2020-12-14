using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.LimbGibbings
{
    public class RagdollComponentController : IND_Mono
    {
        public Rigidbody localRigidBody;
        public Collider localCollider;
        [ShowIf("localCharacterJoint")] public CharacterJoint localCharacterJoint;

        public override void Init()
        {
            DisableRagdoll();
        }

        public void EnableRagdoll()
        {
            localCollider.enabled = true;
            localRigidBody.isKinematic = false;
        }

        public void DisableRagdoll()
        {
            localCollider.enabled = false;
            localRigidBody.isKinematic = true;
        }
        void Reset()
        {
            localRigidBody = GetComponent<Rigidbody>();
            localCollider = GetComponent<Collider>();
            localCharacterJoint = GetComponent<CharacterJoint>();
        }

        public void DestroyRagdollComponent(LimbsController limbController)
        {
            if (localCharacterJoint != null)
            {
                Destroy(localCharacterJoint);
            }

            Destroy(localCollider);
            Destroy(localRigidBody);
            limbController.ragdollComponentControllers.Remove(this);
            Destroy(this);
        }
    }
}