using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;
using IND.Core.Characters.LimbGibbings;
using IND.Core.Characters;
using IND.Core.Weapons;

namespace IND.Core.DevTools
{
    public class IND_SetupCharacterRig : IND_Mono
    {
        [Title("Rig References")]
        [Required] public Transform rightHand;
        [Required] public Transform rightElbow;
        [Required] public Transform rightShoulder;

        [Required] public Transform leftHand;
        [Required] public Transform leftElbow;
        [Required] public Transform leftShoulder;

        [Required] public Transform rightKnee;
        [Required] public Transform rightLegHip;

        [Required] public Transform leftKnee;
        [Required] public Transform leftLegHip;

        [Required] public Transform spine_1;
        [Required] public Transform spine_2;
        [Required] public Transform pelvis;
        [Required] public Transform chest;
        [Required] public Transform neck;
        [Required] public Transform head;

        [Title("Mesh GEO Pieces")]
        [Required] public GameObject headMesh;
        [Required] public GameObject L_ArmMesh;
        [Required] public GameObject R_ArmMesh;
        [Required] public GameObject L_LegMesh;
        [Required] public GameObject R_LegMesh;
        [Required] public GameObject lowerBodyMesh;
        [Required] public GameObject upperBodyMesh;


        [Title("IMPORTANT SETTINGS")]
        public bool overridePreExistingComponentValues = true;
        public bool isPlayer = true;

        private void Start()
        {
            Destroy(this);
        }

        [Button]
        private void SetupRig()
        {
            if (rightHand == null || rightElbow == null || rightShoulder == null)
            {
                Debug.LogError("All Transforms Must Be Assigned Before Proceeding");
                return;
            }

            if (leftHand == null || leftElbow == null || leftShoulder == null)
            {
                Debug.LogError("All Transforms Must Be Assigned Before Proceeding");
                return;
            }

            if (leftLegHip == null || leftKnee == null || rightLegHip == null || rightKnee == null)
            {
                Debug.LogError("All Transforms Must Be Assigned Before Proceeding");
                return;
            }

            if (spine_1 == null || spine_2 == null || chest == null || pelvis == null || neck == null || head == null)
            {
                Debug.LogError("All Transforms Must Be Assigned Before Proceeding");
                return;
            }

            SetupRagdoll();
            SetupHitboxes();
            SetupExtraIdentifiers();
            UpdateHighLevelComponents();
            AddHitboxesToRagdollElements();
        }

        #region UPDATE HIGH LEVEL COMPONENTS
        private void UpdateHighLevelComponents()
        {
            RemoveExistingAnimator();
            AddCharacterLimbController();
            AddHitboxController();
        }

        private void RemoveExistingAnimator()
        {
            Animator[] anims = GetComponentsInChildren<Animator>();
            foreach (Animator item in anims)
            {

                if (gameObject.name != item.gameObject.name)
                {
                    Debug.Log(item.gameObject);
                    DestroyImmediate(item);
                }
            }         
        }

        private void AddHitboxController()
        {
            OldHitboxController hitboxController = GetComponent<OldHitboxController>();
            if(hitboxController == null)
            {
                hitboxController = gameObject.AddComponent<OldHitboxController>();
            }

            hitboxController.GetHitboxHandlers();
        }

        private void AddCharacterLimbController()
        {
            OldLimbsController limbsController = GetComponent<OldLimbsController>();
            if(limbsController == null)
            {
               limbsController = gameObject.AddComponent<OldLimbsController>();
            }

            limbsController.upperBodyMesh = upperBodyMesh;
            limbsController.lowerBodyMesh = lowerBodyMesh;
            limbsController.headMesh = headMesh;
            limbsController.leftArmMesh = L_ArmMesh;
            limbsController.rightArmMesh = R_ArmMesh;
                
            limbsController.head = head;
            limbsController.upperBody = spine_1;
            limbsController.leftArm = leftShoulder;
            limbsController.rightArm = rightShoulder;
            limbsController.pelvis = pelvis;

            limbsController.GetCollidersAndRigidbodies();

        }



        
        #endregion

        #region RAGDOLL FUNCTIONS
        private void SetupRagdoll()
        {
            AddRagdollLimbRigidBody(head, 1.25f, 0, 0.05f);
            AddRagdollCollider(head, ColliderType.SphereCollider, new Vector3(0f, -0.03f, 0f), Vector3.zero, 0.13f, 0f, ColliderDirection.Y);
            AddCharacterLimbIdentifier(head);

            AddRagdollLimbRigidBody(pelvis, 3.125f, 0, 0.05f);
            AddRagdollCollider(pelvis, ColliderType.BoxCollider, new Vector3(-0.29f, 0, 0f), new Vector3(0.38f, 0.44f, 0.29f), 0, 0, ColliderDirection.X);
            AddCharacterLimbIdentifier(pelvis);

            AddRagdollLimbRigidBody(spine_1, 3.125f, 0, 0.05f);
            AddRagdollCollider(spine_1, ColliderType.BoxCollider, new Vector3(0.1f, 0, -0.04f), new Vector3(0.20f, 0.44f, 0.29f), 0, 0, ColliderDirection.X);
            AddCharacterLimbIdentifier(spine_1);

            AddRagdollLimbRigidBody(leftLegHip, 1.875f, 0, 0.05f);
            AddRagdollCollider(leftLegHip, ColliderType.CapsuleCollider, new Vector3(-0.21f, 0f, 0f), Vector3.zero, 0.12f, 0.42f, ColliderDirection.X);
            AddCharacterLimbIdentifier(leftLegHip);

            AddRagdollLimbRigidBody(leftKnee, 1.875f, 0, 0.05f);
            AddRagdollCollider(leftKnee, ColliderType.CapsuleCollider, new Vector3(-0.26f, 0f, 0f), Vector3.zero, 0.13f, 0.52f, ColliderDirection.X);
            AddCharacterLimbIdentifier(leftKnee);

            AddRagdollLimbRigidBody(rightLegHip, 1.875f, 0, 0.05f);
            AddRagdollCollider(rightLegHip, ColliderType.CapsuleCollider, new Vector3(0.21f, 0f, 0f), Vector3.zero, 0.12f, 0.42f, ColliderDirection.X);
            AddCharacterLimbIdentifier(rightLegHip);

            AddRagdollLimbRigidBody(rightKnee, 1.875f, 0, 0.05f);
            AddRagdollCollider(rightKnee, ColliderType.CapsuleCollider, new Vector3(0.26f, 0f, 0f), Vector3.zero, 0.13f, 0.52f, ColliderDirection.X);
            AddCharacterLimbIdentifier(rightKnee);

            AddRagdollLimbRigidBody(leftShoulder, 1.25f, 0, 0.05f);
            AddRagdollCollider(leftShoulder, ColliderType.CapsuleCollider, new Vector3(-0.141f, 0f, 0f), Vector3.zero, 0.07f, 0.28f, ColliderDirection.X);
            AddCharacterLimbIdentifier(leftShoulder);

            AddRagdollLimbRigidBody(leftElbow, 1.25f, 0, 0.05f);
            AddRagdollCollider(leftElbow, ColliderType.CapsuleCollider, new Vector3(-0.141f, 0f, 0f), Vector3.zero, 0.07f, 0.28f, ColliderDirection.X);
            AddCharacterLimbIdentifier(leftElbow);

            AddRagdollLimbRigidBody(rightShoulder, 1.25f, 0, 0.05f);
            AddRagdollCollider(rightShoulder, ColliderType.CapsuleCollider, new Vector3(0.14f, 0f, 0f), Vector3.zero, 0.07f, 0.28f, ColliderDirection.X);
            AddCharacterLimbIdentifier(rightShoulder);

            AddRagdollLimbRigidBody(rightElbow, 1.25f, 0, 0.05f);
            AddRagdollCollider(rightElbow, ColliderType.CapsuleCollider, new Vector3(0.2f, 0f, 0f), Vector3.zero, 0.08f, 0.4f, ColliderDirection.X);
            AddCharacterLimbIdentifier(rightElbow);


            AddRagdollCharacterJoint(head, spine_1, new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, -1), new Vector2(0, 0), new Vector3(-40, 0, 0), new Vector3(25, 0, 0), new Vector2(0, 0), new Vector3(25, 0, 0), new Vector3(0, 0, 0));

            AddRagdollCharacterJoint(rightElbow, rightShoulder, new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector2(0, 0), new Vector3(-90, 0, 0), new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            AddRagdollCharacterJoint(rightShoulder, spine_1, new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector2(0, 0), new Vector3(-70, 0, 0), new Vector3(10, 0, 0), new Vector2(0, 0), new Vector3(50, 0, 0), new Vector3(0, 0, 0));

            AddRagdollCharacterJoint(leftShoulder, spine_1, new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector2(0, 0), new Vector3(-70, 0, 0), new Vector3(10, 0, 0), new Vector2(0, 0), new Vector3(50, 0, 0), new Vector3(0, 0, 0));
            AddRagdollCharacterJoint(leftElbow, leftShoulder, new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector2(0, 0), new Vector3(-90, 0, 0), new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));


            AddRagdollCharacterJoint(spine_1, pelvis, new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 0, -1), new Vector2(0, 0), new Vector3(-20, 0, 0), new Vector3(20, 0, 0), new Vector2(0, 0), new Vector3(10, 0, 0), new Vector3(0, 0, 0));

            AddRagdollCharacterJoint(rightLegHip, pelvis, new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector2(0, 0), new Vector3(-80, 0, 0), new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            AddRagdollCharacterJoint(rightKnee, rightLegHip, new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector2(0, 0), new Vector3(-80, 0, 0), new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            AddRagdollCharacterJoint(leftLegHip, pelvis, new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector2(0, 0), new Vector3(-20, 0, 0), new Vector3(7, 0, 0), new Vector2(0, 0), new Vector3(30, 0, 0), new Vector3(0, 0, 0));
            AddRagdollCharacterJoint(leftKnee, leftLegHip, new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector2(0, 0), new Vector3(-80, 0, 0), new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
        private void AddRagdollLimbRigidBody(Transform target, float rigidMass, float rigidDrag, float angularDrag)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = target.gameObject.AddComponent<Rigidbody>();
            }
            else
            {
                if (overridePreExistingComponentValues == false)
                    return;
            }

            rb.mass = rigidMass;
            rb.drag = rigidDrag;
            rb.angularDrag = angularDrag;
            rb.isKinematic = true;
            rb.useGravity = true;
        }
        private void AddRagdollCollider(Transform target, ColliderType collisionType, Vector3 colliderCentre, Vector3 coliderSize, float colliderRadius, float colliderHeight, ColliderDirection direction)
        {
            Collider collider = target.GetComponent<Collider>();
            if (collider == null)
            {
                switch (collisionType)
                {
                    case ColliderType.BoxCollider:
                        collider = target.gameObject.AddComponent<BoxCollider>();
                        break;
                    case ColliderType.SphereCollider:
                        collider = target.gameObject.AddComponent<SphereCollider>();

                        break;
                    case ColliderType.CapsuleCollider:
                        collider = target.gameObject.AddComponent<CapsuleCollider>();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (overridePreExistingComponentValues == false)
                {
                    return;
                }
            }

            collider.isTrigger = false;

            switch (collisionType)
            {
                case ColliderType.BoxCollider:
                    BoxCollider boxCollision = collider as BoxCollider;
                    boxCollision.center = colliderCentre;
                    boxCollision.size = coliderSize;
                    break;
                case ColliderType.SphereCollider:
                    SphereCollider sphereCollision = collider as SphereCollider;
                    sphereCollision.center = colliderCentre;
                    sphereCollision.radius = colliderRadius;
                    break;
                case ColliderType.CapsuleCollider:
                    CapsuleCollider capsuleCollision = collider as CapsuleCollider;
                    capsuleCollision.center = colliderCentre;
                    capsuleCollision.radius = colliderRadius;
                    capsuleCollision.height = colliderHeight;
                    switch (direction)
                    {
                        case ColliderDirection.X:
                            capsuleCollision.direction = 0;
                            break;
                        case ColliderDirection.Y:
                            capsuleCollision.direction = 1;
                            break;
                        case ColliderDirection.Z:
                            capsuleCollision.direction = 2;
                            break;
                        default:
                            break;
                    }
                    break;
            }

            collider.gameObject.layer = 9;
        }
        private void AddCharacterLimbIdentifier(Transform target)
        {

            LimbIdentifier limbIdentifier = target.GetComponent<LimbIdentifier>();
            if (limbIdentifier == null)
            {
                limbIdentifier = target.gameObject.AddComponent<LimbIdentifier>();
            }
        }
        private void AddRagdollCharacterJoint(Transform target, Transform connectedBody, Vector3 anchor, Vector3 axis, Vector3 swingAxis, Vector2 twistLimitSpring, Vector3 lowTwistLimit, Vector3 highTwistLimit, Vector2 swingLimitSpring, Vector3 swing1Limit, Vector3 swing2Limit)
        {
            CharacterJoint charJoint = target.GetComponent<CharacterJoint>();
            if (charJoint == null)
            {
                charJoint = target.gameObject.AddComponent<CharacterJoint>();
            }
            if (connectedBody != null)
            {
                charJoint.connectedBody = connectedBody.GetComponent<Rigidbody>();
            }
            charJoint.axis = axis;
            charJoint.anchor = anchor;
            charJoint.swingAxis = swingAxis;

            SoftJointLimitSpring twistLimitSprintJoint = charJoint.twistLimitSpring;
            twistLimitSprintJoint.spring = twistLimitSpring.x;
            twistLimitSprintJoint.damper = twistLimitSpring.y;

            SoftJointLimit lowTwistLimitJoint = charJoint.lowTwistLimit;
            lowTwistLimitJoint.limit = lowTwistLimit.x;
            lowTwistLimitJoint.bounciness = lowTwistLimit.y;
            lowTwistLimitJoint.contactDistance = lowTwistLimit.z;

            SoftJointLimit highTwistLimitJoint = charJoint.highTwistLimit;
            highTwistLimitJoint.limit = highTwistLimit.x;
            highTwistLimitJoint.bounciness = highTwistLimit.y;
            highTwistLimitJoint.contactDistance = highTwistLimit.z;

            SoftJointLimitSpring swingLimitSpringJoint = charJoint.swingLimitSpring;
            swingLimitSpringJoint.spring = swingLimitSpring.x;
            swingLimitSpringJoint.damper = swingLimitSpring.y;

            SoftJointLimit swing1LimitJoint = charJoint.swing1Limit;
            swing1LimitJoint.limit = swing1Limit.x;
            swing1LimitJoint.bounciness = swing1Limit.y;
            swing1LimitJoint.contactDistance = swing1Limit.z;

            SoftJointLimit swing2LimitJoint = charJoint.swing2Limit;
            swing2LimitJoint.limit = swing2Limit.x;
            swing2LimitJoint.bounciness = swing2Limit.y;
            swing2LimitJoint.contactDistance = swing2Limit.z;
        }

        #endregion

        #region Hitbox Functions
        private void SetupHitboxes()
        {
            CreateParentHitboxHandler(head, "HITBOX_HANDLER_HEAD", HitboxType.HEAD, head);
            CreateParentHitboxHandler(chest, "HITBOX_HANDLER_LOWER_BODY", HitboxType.LOWER_BODY, spine_1);
            CreateParentHitboxHandler(spine_1, "HITBOX_HANDLER_UPPER_BODY", HitboxType.UPPER_BODY, spine_1);
            CreateParentHitboxHandler(rightShoulder, "HITBOX_HANDLER_R_ARM", HitboxType.R_ARM, rightShoulder);
            CreateParentHitboxHandler(leftShoulder, "HITBOX_HANDLER_L_ARM", HitboxType.L_ARM, leftShoulder);
        }

        private void CreateParentHitboxHandler(Transform parent, string objectName, HitboxType hitboxType, Transform parentRigidbodyTransform)
        {
            HitboxHandler[] handlers = GetComponentsInChildren<HitboxHandler>();
            HitboxHandler hitboxTargetHandler = null;
            foreach (HitboxHandler item in handlers)
            {
                if (item.hitboxType == hitboxType)
                {
                    return;
                }
            }



            GameObject geo = new GameObject();
            geo.transform.SetParent(parent);
            geo.name = objectName;

            geo.transform.localPosition = Vector3.zero;
            geo.transform.localRotation = Quaternion.identity;
            geo.transform.localScale = Vector3.one;

            hitboxTargetHandler = geo.AddComponent<HitboxHandler>();
            hitboxTargetHandler.hitboxType = hitboxType;
            hitboxTargetHandler.parentRigidbody = parentRigidbodyTransform.GetComponent<Rigidbody>();

            switch (hitboxType)
            {
                case HitboxType.HEAD:
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(geo.transform, "HITBOX_HEAD", hitboxTargetHandler, new Vector3(0f, -0.04f, -0.01f), new Vector3(0.19f, 0.27f, 0.21f),Vector3.zero));
                    break;
                case HitboxType.UPPER_BODY:
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(geo.transform, "HITBOX_UPPER_BODY", hitboxTargetHandler, new Vector3(0.018f, -0.004f, -0.01f), new Vector3(0.19f, 0.27f, 0.21f),Vector3.zero));
                    break;
                case HitboxType.L_ARM:
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(leftElbow.transform, "HITBOX_L_Elbow", hitboxTargetHandler, new Vector3(-0.066f, 0.01f, 0.009f), new Vector3(0.288f, 0.095f, 0.08f), Vector3.zero));
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(leftShoulder.transform, "HITBOX_L_Shoulder", hitboxTargetHandler, new Vector3(-0.093f, 0.001f, -0.0101f), new Vector3(0.328f, 0.134f, 0.094f), Vector3.zero));
                    break;
                case HitboxType.R_ARM:
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(rightElbow.transform, "HITBOX_R_Elbow", hitboxTargetHandler, new Vector3(0.1f, 0.006f, 0f), new Vector3(0.288f, 0.085f, 0.07f), Vector3.zero));
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(rightShoulder.transform, "HITBOX_R_Shoulder", hitboxTargetHandler, new Vector3(0.089f, 0.004f, 0.004f), new Vector3(0.359f, 0.103f, 0.111f), Vector3.zero));
                    break;
                case HitboxType.PROP:
                    break;
                case HitboxType.LOWER_BODY:
                    hitboxTargetHandler.hitboxColliders.Add(CreateHitboxCollider(geo.transform, "HITBOX_Lower_BODY", hitboxTargetHandler, new Vector3(-0.128f, -0.01f, 0.001f), new Vector3(0.39f, 0.33f, 0.20f), Vector3.zero));
                    break;
            }
        }

        private Collider CreateHitboxCollider(Transform parent, string objectName, HitboxHandler parentHandler, Vector3 colliderCentre, Vector3 colliderSize, Vector3 localRotation)
        {
            GameObject geo = new GameObject();
            geo.name = objectName;
            geo.transform.SetParent(parent);

            geo.transform.localPosition = Vector3.zero;
            Quaternion rot = Quaternion.Euler(localRotation);
            geo.transform.localRotation = rot;
            geo.transform.localScale = Vector3.one;

            HitboxColliderHandler colliderHandler = geo.AddComponent<HitboxColliderHandler>();
            colliderHandler.parentHandler = parentHandler;
            BoxCollider boxCol = geo.AddComponent<BoxCollider>();
            boxCol.isTrigger = true;
            boxCol.center = colliderCentre;
            boxCol.size = colliderSize;

            if (isPlayer == true)
            {
                geo.layer = 10;
            }
            else
            {
                geo.layer = 11;
            }

            return boxCol;
        }
        #endregion

        #region EXTRA IDENTIFIERS FUNCTIONS
        private void SetupExtraIdentifiers()
        {
            SetupRightHand();
            SetupFists();
        }

        private void SetupRightHand()
        {
            RightHandIdentifier rightHandIdentifier = rightHand.GetComponent<RightHandIdentifier>();
            if (rightHandIdentifier == null)
            {
                rightHand.gameObject.AddComponent<RightHandIdentifier>();
            }
        }

        private void SetupFists()
        {
            WeaponFistsIdentifier fistsIdentifier = GetComponentInChildren<WeaponFistsIdentifier>();
            if (fistsIdentifier == null)
            {
                GameObject fistsGeoHolder = new GameObject();
                fistsGeoHolder.transform.SetParent(transform);
                fistsGeoHolder.transform.localPosition = Vector3.zero;
                fistsGeoHolder.transform.localRotation = Quaternion.identity;
                fistsGeoHolder.transform.localScale = Vector3.one;
                fistsGeoHolder.gameObject.name = "Fists Weapon Holder";
                fistsGeoHolder.AddComponent<WeaponFistsIdentifier>();
            }

            WeaponItemRuntimeMelee fistsMeleeRuntime = GetComponentInChildren<WeaponFistsIdentifier>().GetComponent<WeaponItemRuntimeMelee>();
            if (fistsMeleeRuntime == null)
            {
                fistsMeleeRuntime = GetComponentInChildren<WeaponFistsIdentifier>().gameObject.AddComponent<WeaponItemRuntimeMelee>();
            }

            if (isPlayer == true)
            {
                fistsMeleeRuntime.isPlayerWeapon = true;
                fistsMeleeRuntime.gameObject.layer = 18;
            }
            else
            {
                fistsMeleeRuntime.isPlayerWeapon = false;
                fistsMeleeRuntime.gameObject.layer = 13;
            }

            fistsMeleeRuntime.weaponItemData = Resources.Load("Weapons/Weapon Types/Weapon_Melee_Fists") as WeaponItem;
            fistsMeleeRuntime.attackColliders.Clear();

            BoxCollider rightHandCollider = rightHand.GetComponent<BoxCollider>();
            if (rightHandCollider == null)
            {
                rightHandCollider = rightHand.gameObject.AddComponent<BoxCollider>();
                rightHandCollider.center = new Vector3(0.066f, -0.015f, -0.012f);
                rightHandCollider.size = new Vector3(0.383f, 0.163f, 0.112f);
                rightHandCollider.enabled = false;

                WeaponMeleeAttackCollider attackColliderRight = rightHand.gameObject.AddComponent<WeaponMeleeAttackCollider>();
                attackColliderRight.meleeItem = fistsMeleeRuntime;
                fistsMeleeRuntime.attackColliders.Add(attackColliderRight);
            }

            BoxCollider leftHandCollider = leftHand.GetComponent<BoxCollider>();
            if (leftHandCollider == null)
            {
                leftHandCollider = leftHand.gameObject.AddComponent<BoxCollider>();
                leftHandCollider.center = new Vector3(-0.041f, -0.040f, 0.0104f);
                leftHandCollider.size = new Vector3(0.366f, 0.1251f, 0.170f);
                leftHandCollider.enabled = false;

                WeaponMeleeAttackCollider attackColliderLeft = leftHand.gameObject.AddComponent<WeaponMeleeAttackCollider>();
                attackColliderLeft.meleeItem = fistsMeleeRuntime;
                fistsMeleeRuntime.attackColliders.Add(attackColliderLeft);
            }

        }




        [Button]
        private void DestroyExistingComponents()
        {
            OldLimbsController limbController = GetComponent<OldLimbsController>();
            if(limbController != null)
            {
                DestroyImmediate(limbController);
            }

            OldHitboxController hitboxController = GetComponent<OldHitboxController>();
            if(hitboxController != null)
            {
                DestroyImmediate(hitboxController);
            }

            CharacterJoint[] charJoint = GetComponentsInChildren<CharacterJoint>();
            foreach (CharacterJoint item in charJoint)
            {
                DestroyImmediate(item);
            }

            Rigidbody[] rigidbody = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody item in rigidbody)
            {
                DestroyImmediate(item);
            }

            Collider[] collisions = GetComponentsInChildren<Collider>();
            foreach (Collider item in collisions)
            {
                DestroyImmediate(item);
            }

            LimbIdentifier[] limbIdentifiers = GetComponentsInChildren<LimbIdentifier>();
            foreach (LimbIdentifier item in limbIdentifiers)
            {
                DestroyImmediate(item);
            }

            HitboxColliderHandler[] colliderHandlers = GetComponentsInChildren<HitboxColliderHandler>();
            foreach (HitboxColliderHandler item in colliderHandlers)
            {
                DestroyImmediate(item.gameObject);
            }

            HitboxHandler[] hitboxHandlers = GetComponentsInChildren<HitboxHandler>();
            foreach (HitboxHandler item in hitboxHandlers)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        #endregion

        void AddHitboxesToRagdollElements()
        {
            AddHitboxToTargetRagdoll(head, HitboxType.HEAD);
            AddHitboxToTargetRagdoll(spine_1, HitboxType.UPPER_BODY);
            AddHitboxToTargetRagdoll(pelvis, HitboxType.UPPER_BODY);
            AddHitboxToTargetRagdoll(leftElbow, HitboxType.L_ARM);
            AddHitboxToTargetRagdoll(leftShoulder, HitboxType.L_ARM);

        }

        void AddHitboxToTargetRagdoll(Transform targetTransform, HitboxType hitboxType)
        {
          HitboxColliderHandler colliderHandler = targetTransform.gameObject.AddComponent<HitboxColliderHandler>();

            HitboxHandler[] allHandlers = GetComponentsInChildren<HitboxHandler>();
            foreach (HitboxHandler item in allHandlers)
            {
                if(item.hitboxType == hitboxType)
                {
                    colliderHandler.parentHandler = item;
                }
            }
            
        }
    }

    [System.Serializable]
    public enum ColliderType
    {
        BoxCollider, SphereCollider, CapsuleCollider
    }
    [System.Serializable]
    public enum ColliderDirection
    {
        X, Y, Z
    }

}