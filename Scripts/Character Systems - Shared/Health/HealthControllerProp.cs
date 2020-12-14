using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Characters.Hitboxes;

namespace IND.Core.Characters.Health{
public class HealthControllerProp : HealthController
{

        [Button]
        public void TestAddForce()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 1000f, ForceMode.Impulse);
        }
        public override void Death(HitboxType hitboxType)
        {
            base.Death(hitboxType);

            Destroy(gameObject);
        }
    }
}