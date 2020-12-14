using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared
{
    public class DestroyTimer : MonoBehaviour
    {
        [InfoBox("This script can either use the assigned value or it can be initialized with another value from another script")]
        public float timerToDestroy = 1f;
 
        public void InitializeWithNewTimer(float timerBeforeDestroy)
        {
            timerToDestroy = timerBeforeDestroy;
        }
        public void Start()
        {
            StartCoroutine("StartTimer");
        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(timerToDestroy);
            DestroyGameObject();
        }

        private void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}