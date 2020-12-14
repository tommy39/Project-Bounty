using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems.TroopSystems
{
    [CreateAssetMenu(fileName = "Troop_AI_Nav_Mesh_Settings", menuName = "IND/Core/AI/Nav Mesh Settings")]

    public class AINavMeshSettings : ScriptableObject
    {
        [Title("Steering")]
        public float speed = 3.5f;
        public float angularSpeed = 120f;
        public float acceleration = 8f;
        public float stoppingDistance = 0.01f;
        public bool autoBraking = true;

        [Title("Obstacle Avoidance")]
        public float radius = 0.5f;
        public float height = 2f;
        public ObstacleAvoidanceType avoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        public int avoidancePriority = 50;
    }
}