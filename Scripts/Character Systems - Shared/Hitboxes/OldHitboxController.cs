using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Shared.Statics;

namespace IND.Core.Characters.Hitboxes
{
    public class OldHitboxController : IND_Mono
    {
        public bool isPlayer = false;

        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler headHitboxHandler;
        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler torsoHitboxHandler;
        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler leftArmHitboxHandler;
        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler rightArmHitboxHandler;
        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler leftLegHitboxHandler;
        [Required] [InlineEditor] [FoldoutGroup("Hitbox Handlers")] public HitboxHandler rightLegHitboxHandler;
        public override void Init()
        {
            AssignLayerToColliders();
        }

        [Button]
        public void GetHitboxHandlers()
        {
            HitboxHandler[] handlers = GetComponentsInChildren<HitboxHandler>();
            foreach (HitboxHandler item in handlers)
            {
                switch (item.hitboxType)
                {
                    case HitboxType.HEAD:
                        headHitboxHandler = item;
                        break;
                    case HitboxType.UPPER_BODY:
                        torsoHitboxHandler = item;
                        break;
                    case HitboxType.L_ARM:
                        leftArmHitboxHandler = item;
                        break;
                    case HitboxType.R_ARM:
                        rightArmHitboxHandler = item;
                        break;
                }
            }
        }


        [Button]
        public void EnableHitboxes()
        {
            headHitboxHandler.EnableColliders();
            torsoHitboxHandler.EnableColliders();
            leftArmHitboxHandler.EnableColliders();
            leftLegHitboxHandler.EnableColliders();
            rightArmHitboxHandler.EnableColliders();
            rightLegHitboxHandler.EnableColliders();
        }

        [Button]
        public void DisableHitboxes()
        {
            headHitboxHandler.DisableColliders();
            torsoHitboxHandler.DisableColliders();
            leftArmHitboxHandler.DisableColliders();
            leftLegHitboxHandler.DisableColliders();
            rightArmHitboxHandler.DisableColliders();
            rightLegHitboxHandler.DisableColliders();
        }

        void AssignLayerToColliders()
        {
            int layerNumber = 0;

            if(isPlayer == true)
            {
                ScriptableLayerMask playerLayerMask = Resources.Load("LayerMasks/LayerMask_PlayerHitbox") as ScriptableLayerMask;
                layerNumber = GetLayerFromLayerMask.GetInt(playerLayerMask.value);
            }
            else
            {
                ScriptableLayerMask enemyLayerMask = Resources.Load("LayerMasks/LayerMask_EnemyHitbox") as ScriptableLayerMask;
                layerNumber = GetLayerFromLayerMask.GetInt(enemyLayerMask.value);
            }
            

            headHitboxHandler.AssignLayerMask(layerNumber);
            torsoHitboxHandler.AssignLayerMask(layerNumber);
            leftArmHitboxHandler.AssignLayerMask(layerNumber);
            rightArmHitboxHandler.AssignLayerMask(layerNumber);
            leftLegHitboxHandler.AssignLayerMask(layerNumber);
            rightLegHitboxHandler.AssignLayerMask(layerNumber);
        }

        [Button]
        void CheckIfAllHitboxesHaveEffectController()
        {
            headHitboxHandler.MakeSureHasEffectController();
            torsoHitboxHandler.MakeSureHasEffectController();
            leftArmHitboxHandler.MakeSureHasEffectController();
            rightArmHitboxHandler.MakeSureHasEffectController();
            leftLegHitboxHandler.MakeSureHasEffectController();
            rightLegHitboxHandler.MakeSureHasEffectController();
        }
    }
}