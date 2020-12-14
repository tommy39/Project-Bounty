using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Characters.Hitboxes
{
    public class HitboxColliderHandler : IND_Mono
    {
        [Required] public HitboxHandler parentHandler;
    }
}