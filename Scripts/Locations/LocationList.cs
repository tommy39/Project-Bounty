using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Locations
{
    [CreateAssetMenu(fileName = "Location List", menuName = "IND/Tools/Locations/LocationList")]
    public class LocationList : ScriptableObject
    {
        public List<Location> locations = new List<Location>();

    }
}