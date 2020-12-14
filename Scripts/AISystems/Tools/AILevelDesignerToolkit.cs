using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.Shared;
using IND.Core.AISystems.FieldOfView;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems
{
    [ExecuteInEditMode]
    public class AILevelDesignerToolkit : MonoBehaviour
    {
        #region ClassReferences
        private FieldOfViewController fieldOfViewController;

        #endregion


        public int attackRangeModifier;

        [Title("Field Of View")]
        public bool useFieldOfView = false;
        [ShowIf("useFieldOfView")] [Range(0, 30)] public float fieldOfViewDistance;
        [ShowIf("useFieldOfView")] [SerializeField] [Range(0, 360)] protected int fieldOfViewAngle;
        void Start()
        {
            HandleFieldOfView();
        }

        private void HandleFieldOfView()
        {
            if (useFieldOfView)
            {
                if (fieldOfViewController == null)
                {
                    fieldOfViewController = GetComponentInChildren<FieldOfViewController>();
                }
                fieldOfViewController.viewAngle = fieldOfViewAngle;
                fieldOfViewController.viewDistance = fieldOfViewDistance;
            }
        }

        private void OnValidate()
        {
            Start();
        }

    }
}