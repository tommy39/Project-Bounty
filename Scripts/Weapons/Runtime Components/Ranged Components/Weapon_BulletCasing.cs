using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    public class Weapon_BulletCasing : IND_Mono
    {
        public float durationBeforeMakeStatic = 7f;
        public float thrustAmount = 1f;

        public Rigidbody rigidBody;
        public BoxCollider boxCollider;

        public List<GameObject> nonCasingElementsToDestroy = new List<GameObject>();
        private void GetComponents()
        {
            rigidBody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            rigidBody.AddForce(transform.forward * thrustAmount);
            StartCoroutine("TimerToDestroy");
        }
        public IEnumerator TimerToDestroy()
        {
            yield return new WaitForSeconds(durationBeforeMakeStatic);
            Destroy(rigidBody);
            Destroy(boxCollider);
            for (int i = 0; i < nonCasingElementsToDestroy.Count; i++)
            {
                Destroy(nonCasingElementsToDestroy[i]);
            }
            Destroy(this);
        }

        private void Reset()
        {
            if(GetComponent<Rigidbody>() == null)
            {
               rigidBody = gameObject.AddComponent<Rigidbody>();
            }

            if(GetComponent<BoxCollider>() == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider>();
            }

            GetComponents();            
        }
    }
}