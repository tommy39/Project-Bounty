using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Shared.Statics;

namespace IND.Core.Characters.LimbGibbings
{
    public class OldLimbsController : IND_Mono
    {
        [Required] [InlineEditor] public LimbsData limbsData;

        [FoldoutGroup("Limb Meshes")] public GameObject upperBodyMesh;
        [FoldoutGroup("Limb Meshes")] public GameObject lowerBodyMesh;
        [FoldoutGroup("Limb Meshes")] public GameObject headMesh;
        [FoldoutGroup("Limb Meshes")] public GameObject leftArmMesh;
        [FoldoutGroup("Limb Meshes")] public GameObject rightArmMesh;

        [FoldoutGroup("Limb Parts")] public Transform upperBody;
        [FoldoutGroup("Limb Parts")] public Transform pelvis;
        [FoldoutGroup("Limb Parts")] public Transform head;
        [FoldoutGroup("Limb Parts")] public Transform leftArm;
        [FoldoutGroup("Limb Parts")] public Transform rightArm;
        [FoldoutGroup("Limb Parts")] public Transform spine_1;

        [FoldoutGroup("Limb Colliders")] public Collider upperBodyCollider;
        [FoldoutGroup("Limb Colliders")] public Collider headCollider;
        [FoldoutGroup("Limb Colliders")] public Collider centreCollider;
        [FoldoutGroup("Limb Colliders")] public List<Collider> leftArmColliders = new List<Collider>();
        [FoldoutGroup("Limb Colliders")] public List<Collider> rightArmColliders = new List<Collider>();
        [FoldoutGroup("Limb Colliders")] public List<Collider> leftLegColliders = new List<Collider>();
        [FoldoutGroup("Limb Colliders")] public List<Collider> rightLegColliders = new List<Collider>();

        [FoldoutGroup("Limb Rigidbodies")] public Rigidbody upperBodyRigidBody;
        [FoldoutGroup("Limb Rigidbodies")] public Rigidbody headRigidbody;
        [FoldoutGroup("Limb Rigidbodies")] public Rigidbody centreRigidbody;
        [FoldoutGroup("Limb Rigidbodies")] public List<Rigidbody> leftArmRigidbodies = new List<Rigidbody>();
        [FoldoutGroup("Limb Rigidbodies")] public List<Rigidbody> rightArmRigidbodies = new List<Rigidbody>();
        [FoldoutGroup("Limb Rigidbodies")] public List<Rigidbody> leftLegRigidbodies = new List<Rigidbody>();
        [FoldoutGroup("Limb Rigidbodies")] public List<Rigidbody> rightLegRigidbodies = new List<Rigidbody>();
        public override void Init()
        {
            SetupRagdoll();
            DisableEntireRagdoll();
        }
        void SetupRagdoll()
        {
            int layerMask = GetLayerFromLayerMask.GetInt(limbsData.limbsRagdollMask.value);

            upperBodyRigidBody.gameObject.layer = layerMask;
            headRigidbody.gameObject.layer = layerMask;
            centreRigidbody.gameObject.layer = layerMask;

            for (int i = 0; i < leftArmRigidbodies.Count; i++)
            {
                leftArmRigidbodies[i].gameObject.layer = layerMask;
            }
            for (int i = 0; i < rightArmRigidbodies.Count; i++)
            {
                rightArmRigidbodies[i].gameObject.layer = layerMask;
            }
            for (int i = 0; i < leftLegRigidbodies.Count; i++)
            {
                leftLegRigidbodies[i].gameObject.layer = layerMask;
            }
            for (int i = 0; i < rightLegRigidbodies.Count; i++)
            {
                rightLegRigidbodies[i].gameObject.layer = layerMask;
            }
        }

        [Button]
        public void EnableEntireRagdoll()
        {
            ChangeRagdollState(false);
            ChangeRagdollCollidersState(true);
        }

        [Button]
        public void DisableEntireRagdoll()
        {
            ChangeRagdollState(true);
            ChangeRagdollCollidersState(false);
        }

        void ChangeRagdollState(bool value)
        {
            upperBodyRigidBody.isKinematic = value;
            headRigidbody.isKinematic = value;
            centreRigidbody.isKinematic = value;

            for (int i = 0; i < leftArmRigidbodies.Count; i++)
            {
                leftArmRigidbodies[i].isKinematic = value;
            }
            for (int i = 0; i < rightArmRigidbodies.Count; i++)
            {
                rightArmRigidbodies[i].isKinematic = value;
            }
            for (int i = 0; i < leftLegRigidbodies.Count; i++)
            {
                leftLegRigidbodies[i].isKinematic = value;
            }
            for (int i = 0; i < rightLegRigidbodies.Count; i++)
            {
                rightLegRigidbodies[i].isKinematic = value;
            }
        }

        void ChangeRagdollCollidersState(bool value)
        {
            upperBodyCollider.enabled = value;
            headCollider.enabled = value;
            centreCollider.enabled = value;

            for (int i = 0; i < leftArmRigidbodies.Count; i++)
            {
                leftArmColliders[i].enabled = value;
            }
            for (int i = 0; i < rightArmRigidbodies.Count; i++)
            {
                rightArmColliders[i].enabled = value;
            }
            for (int i = 0; i < leftLegRigidbodies.Count; i++)
            {
                leftLegColliders[i].enabled = value;
            }
            for (int i = 0; i < rightLegRigidbodies.Count; i++)
            {
                rightLegColliders[i].enabled = value;
            }
        }

        [Button]
        public void GetCollidersAndRigidbodies()
        {
            upperBodyCollider = GetSingleCollider(upperBody);
            headCollider = GetSingleCollider(head);
            centreCollider = GetSingleCollider(pelvis);
            GetCollidersChildren(leftArm, leftArmColliders);
            GetCollidersChildren(rightArm, rightArmColliders);

            upperBodyRigidBody = GetSingleRigidBody(upperBody);
            headRigidbody = GetSingleRigidBody(head);
            centreRigidbody = GetSingleRigidBody(pelvis);
            GetRigidBodiesChildren(leftArm, leftArmRigidbodies);
            GetRigidBodiesChildren(rightArm, rightArmRigidbodies);
        }

        private Collider GetSingleCollider(Transform target)
        {
            return target.GetComponent<Collider>();
        }

        private void GetCollidersChildren(Transform parentGameObject, List<Collider> colliderList)
        {
            colliderList.Clear();
            LimbIdentifier[] limbIdentifiers = parentGameObject.GetComponentsInChildren<LimbIdentifier>();
            foreach (LimbIdentifier item in limbIdentifiers)
            {
                colliderList.Add(item.GetComponent<Collider>());
            }
        }

        private Rigidbody GetSingleRigidBody(Transform target)
        {
            return target.GetComponent<Rigidbody>();
        }

        private void GetRigidBodiesChildren(Transform target, List<Rigidbody> rigidbodiesList)
        {
            rigidbodiesList.Clear();
            Rigidbody[] rigids = target.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody item in rigids)
            {
                rigidbodiesList.Add(item);
            }
        }

        #region Destroy Body Parts Functionality
        public void DetachLeftArm()
        {
            leftArmMesh.gameObject.SetActive(false);
            for (int i = 0; i < leftArmColliders.Count; i++)
            {
                leftArmColliders[i].enabled = false;
            }

            leftArm.gameObject.SetActive(false);

            SpawnBodyPart(limbsData.leftArmPrefab, leftArm.position);
        }

        public void DetachRightArm()
        {
            rightArmMesh.gameObject.SetActive(false);
            for (int i = 0; i < rightArmColliders.Count; i++)
            {
                rightArmColliders[i].enabled = false;
            }

            rightArm.gameObject.SetActive(false);

            SpawnBodyPart(limbsData.rightArmPrefab, rightArm.position);
        }     

        public void DetachTorsoWithUpperBody() //Should spawn the torso with arms and heads 
        {
            upperBodyMesh.gameObject.SetActive(false);

            upperBodyCollider.enabled = false;

            upperBody.gameObject.SetActive(false);

            SpawnBodyPart(limbsData.torsoPrefab, upperBody.position);
        }

        public void DetachHead()
        {
            headMesh.gameObject.SetActive(false);

            headCollider.enabled = false;

            head.gameObject.SetActive(false);

            SpawnBodyPart(limbsData.headPrefab, head.position);
        }

        #endregion

        void SpawnBodyPart(GameObject bodyMeshToSpawn, Vector3 position)
        {
            GameObject geo = Instantiate(bodyMeshToSpawn);
            geo.transform.position = position;
        }
    }
}