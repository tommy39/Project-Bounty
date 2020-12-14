using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.Player;
using Sirenix.OdinInspector;
using IND.Core.Shared.Statics;

namespace IND.Core.Characters.Hitboxes
{
    [RequireComponent(typeof(BoxCollider))]
    public class SubHitboxController : IND_Mono
    {
        public HitboxType hitboxType;
        private HitboxController hitboxController;
        [HideInInspector] public BoxCollider collision;
        public override void Init()
        {
            hitboxController = GetComponentInParent<HitboxController>();
            collision = GetComponent<BoxCollider>();

            PlayerController playerController = GetComponentInParent<PlayerController>();
            if(playerController != null)
            {
                ScriptableLayerMask playerLayerMask = Resources.Load("LayerMasks/LayerMask_PlayerHitbox") as ScriptableLayerMask;
                gameObject.layer = GetLayerFromLayerMask.GetInt(playerLayerMask.value);
            }
            else
            {
                ScriptableLayerMask enemyLayerMask = Resources.Load("LayerMasks/LayerMask_EnemyHitbox") as ScriptableLayerMask;
                gameObject.layer = GetLayerFromLayerMask.GetInt(enemyLayerMask.value);
            }
        }

        private void OnValidate()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }

    
}