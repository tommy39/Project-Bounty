using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.Locations;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace IND.Core
{
    public class AutoBuildLoader : MonoBehaviour
    {
        [Required] public Location targetLocationToLoad;
        void Start()
        {
            LoadTargetLocation();
        }

        void LoadTargetLocation()
        {
            string selectedLocationName = targetLocationToLoad.locationName;

            SceneManager.LoadScene("MasterScene");
            SceneManager.LoadScene(selectedLocationName + "_Settings", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Gameplay", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Ground", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Props", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Buildings", LoadSceneMode.Additive);
            SceneManager.LoadScene(selectedLocationName + "_Lighting", LoadSceneMode.Additive);
        }
    }
}