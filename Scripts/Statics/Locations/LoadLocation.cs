using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using IND.Core.Locations;
using IND.Core.Managers.Level;

namespace IND.Core.Statics.Locations
{
    public static class LoadLocation
    {
        public static void Execute(Location nextLocationData)
        {
            string selectedLocationName = nextLocationData.locationName;
            //Need to store the Master Scene Data

            SceneManager.LoadScene(selectedLocationName + "_Settings", LoadSceneMode.Single);
            SceneManager.LoadScene(selectedLocationName + "_Gameplay", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Ground", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Props", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Buildings", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Lighting", LoadSceneMode.Additive);
        }
    }
}