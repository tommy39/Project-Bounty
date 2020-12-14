using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    public class Weapon_ThrowableMagazine : IND_Mono
    {
        public float durationBeforeMakeStatic = 7f;
        public float thrustAmount = 1f;

        public Rigidbody rigid;
        public BoxCollider col;
       
        [Button]
        private void GetComponents()
        {
            rigid = GetComponent<Rigidbody>();
            col = GetComponent<BoxCollider>();
            if(col == null)
            {
                col = GetComponentInChildren<BoxCollider>();
            }
        }
        private void Start()
        {
            rigid.AddForce(transform.forward * thrustAmount);
            StartCoroutine("TimerToDestroy");
        }
        public IEnumerator TimerToDestroy()
        {
            yield return new WaitForSeconds(durationBeforeMakeStatic);
            Destroy(rigid);
            Destroy(col);
            Destroy(this);
        }

    }
}