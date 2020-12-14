using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.AISystems;
using IND.Core.Player;
using IND.Core.AISystems.Inventory;
using IND.Core.Characters.Health;
using IND.Core.AISystems.States;

namespace IND.Core.AISystems.FieldOfView
{
    public class FieldOfViewController : IND_Mono
    {
        private AIController aiController;
        private AIInventoryController inventoryController;
        private HealthControllerAI aiHealthController;
        private AIAlertController alertController;
        private AIKnockDownController knockdownController;
        private AIStateController stateController;

        [HideInInspector] public HealthControllerPlayer playerTarget;

        [Required] [InlineEditor] public FieldOfViewData data;
        [Required] public Transform castPoint;

        [Range(0, 30)] public float viewDistance = 7f;
        [Range(0, 360)] public int viewAngle = 58;

        MeshFilter viewMeshFilter;
        Mesh viewMesh;

        [Required] [InlineEditor] public ScriptableBool drawFieldOfViewMesh;
        [Required] [InlineEditor] public ScriptableBool alwaysDrawFieldOfViewGizmo;
        [Required] [InlineEditor] public ScriptableBool drawWireSphere;
        public bool useDebugs = false;

        private int currentFrameTick;
        public Collider[] collisionsInSphereCast;

        RaycastHit fieldOFViewRay;
        public bool playerFound = false;

        Vector3 desiredRayCastPosition;
        public override void Init()
        {
            viewMeshFilter = GetComponent<MeshFilter>();
            aiController = GetComponentInParent<AIController>();
            playerTarget = FindObjectOfType<HealthControllerPlayer>();
            inventoryController = aiController.GetComponent<AIInventoryController>();
            alertController = aiController.GetComponent<AIAlertController>();
            aiHealthController = aiController.GetComponent<HealthControllerAI>();
            stateController = aiController.GetComponent<AIStateController>();
            knockdownController = aiController.GetComponent<AIKnockDownController>();

            viewMesh = new Mesh();
            viewMesh.name = "View Mesh";
            viewMeshFilter.mesh = viewMesh;

            if (drawFieldOfViewMesh.value)
            {
                DrawFieldOfView();
            }
        }

        public override void Tick()
        {
            if (currentFrameTick < data.tickRate)
            {
                currentFrameTick++;
                return;
            }
            else
            {
                currentFrameTick = 0;
            }

            GetSphereCastTargets();
            GetFieldOfViewTargets();
        }

        void GetSphereCastTargets()
        {
            collisionsInSphereCast = Physics.OverlapSphere(transform.position, viewDistance, data.targetMask.value);
        }

        void GetFieldOfViewTargets()
        {
            playerFound = false;

            desiredRayCastPosition = castPoint.position;

            for (int i = 0; i < collisionsInSphereCast.Length; i++)
            {
                Transform target = collisionsInSphereCast[i].transform;

                Vector3 dirToTarget = Vector3.zero;

                bool isInViewAngle = CheckIsTargetInFov(target, out dirToTarget);

                if (!isInViewAngle)
                {
                    if (useDebugs)
                    {
                        Debug.Log("Not In View Angle");
                    }

                    if (playerFound == false && playerTarget != null)
                    {
                        PlayerLeftFOV();
                    }
                    return;
                }

                if (Physics.Raycast(desiredRayCastPosition, dirToTarget, out fieldOFViewRay, viewDistance, data.obstacleMask.value))
                {
                    if (useDebugs)
                    {
                        Debug.Log("Hitting Obstacle");
                    }

                    if (fieldOFViewRay.collider.GetComponent<HealthControllerPlayer>() != null)
                    {
                        playerTarget = fieldOFViewRay.transform.GetComponent<HealthControllerPlayer>();
                        playerFound = true;
                        PlayerEnteredFOV();
                        return;
                    }
                }
                else
                {
                    //Did Not Obstacle

                    if (useDebugs)
                    {
                        Debug.Log("Not Hitting Obstacle");
                    }
                }
            }

            if (collisionsInSphereCast.Length == 0)
            {
                playerFound = false;
            }
        }

        void PlayerEnteredFOV()
        {
            if(stateController.currentState != AIStateType.COMBAT)
            {
                if (stateController.currentState == AIStateType.KNOCKEDDOWN)
                    return;

                stateController.ChangeState(AIStateType.COMBAT);
            }
        }

        void PlayerLeftFOV()
        {
            playerTarget = null;
        }
        bool CheckIsTargetInFov(Transform target, out Vector3 dirToTarget)
        {
            dirToTarget = (new Vector3(target.position.x, castPoint.position.y, target.position.z) - castPoint.position).normalized;
            Debug.DrawRay(castPoint.position, dirToTarget * 10f, Color.red);
            if (Vector3.Angle(castPoint.forward, dirToTarget) < viewAngle / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (alwaysDrawFieldOfViewGizmo.value == true)
                return;

            DrawFieldOfViewGizmo();

        }

        private void OnDrawGizmos()
        {
            if (alwaysDrawFieldOfViewGizmo.value == false)
                return;

            DrawFieldOfViewGizmo();
        }

        void DrawFieldOfViewGizmo()
        {
            Gizmos.color = Color.yellow;

            if (drawWireSphere.value)
            {
                Gizmos.DrawWireSphere(transform.position, viewDistance);
            }

            Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.forward * viewDistance;
            Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewDistance;

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(castPoint.position, fovLine1);

            Vector3 endPoint1 = (castPoint.position) + fovLine1;
            Vector3 endPoint2 = (castPoint.position) + fovLine2;
            Gizmos.DrawLine(castPoint.position, endPoint2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(endPoint1, endPoint2);
        }

        #region Runtime Field of View Mesh
        void DrawFieldOfView()
        {
            int stepCount = Mathf.RoundToInt(viewAngle * data.meshResolution);
            float stepAngleSize = viewAngle / stepCount;
            List<Vector3> viewPoints = new List<Vector3>();

            ViewCastInfo oldViewCastInfo = new ViewCastInfo();


            for (int i = 0; i < stepCount; i++)
            {
                float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle);


                if (i > 0)
                {
                    bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCastInfo.distance - newViewCast.distance) > data.edgeDistanceThreshold;
                    if (oldViewCastInfo.hit != newViewCast.hit || (oldViewCastInfo.hit && newViewCast.hit && edgeDistanceThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(oldViewCastInfo, newViewCast);
                        if (edge.pointA != Vector3.zero)
                        {
                            viewPoints.Add(edge.pointA);
                        }
                        if (edge.pointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.pointB);
                        }
                    }
                }

                viewPoints.Add(newViewCast.point);
            }

            int vertexCount = viewPoints.Count + 1;
            Vector3[] verticies = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            verticies[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                verticies[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

                if (i < vertexCount - 2)
                {

                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            viewMesh.Clear();
            viewMesh.vertices = verticies;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();

        }

        EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            float minAngle = minViewCast.angle;
            float maxAngle = maxViewCast.angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;

            for (int i = 0; i < data.edgeResolveIterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(angle);
                bool edgeDistanceThresholdExceeded = Mathf.Abs(minViewCast.distance - newViewCast.distance) > data.edgeDistanceThreshold;

                if (newViewCast.hit == minViewCast.hit && !edgeDistanceThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }

        ViewCastInfo ViewCast(float globalAngle)
        {
            Vector3 dir = DirFromAngle(globalAngle, true);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, viewDistance, data.obstacleMask.value))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, transform.position + dir * viewDistance, viewDistance, globalAngle);
            }
        }



        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public struct ViewCastInfo
        {
            public bool hit;
            public Vector3 point;
            public float distance;
            public float angle;

            public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
            {
                hit = _hit;
                point = _point;
                distance = _distance;
                angle = _angle;
            }
        }

        public struct EdgeInfo
        {
            public Vector3 pointA;
            public Vector3 pointB;

            public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
            {
                pointA = _pointA;
                pointB = _pointB;
            }
        }
        #endregion
    }
}