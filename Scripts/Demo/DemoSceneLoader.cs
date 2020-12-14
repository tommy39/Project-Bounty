using IND.Core.Locations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IND.Core.Demo {
    public class DemoSceneLoader : MonoBehaviour
    {
        [SerializeField] protected Location locationData;
        void Awake()
        {
            SceneManager.LoadScene("LevelType_Gameplay", LoadSceneMode.Single);
            SceneManager.LoadScene(locationData.locationName + "_Settings", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Gameplay", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Ground", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Props", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Buildings", LoadSceneMode.Additive);
            SceneManager.LoadScene(locationData.locationName + "_Lighting", LoadSceneMode.Additive);
            SceneManager.LoadScene("MasterScene", LoadSceneMode.Additive);
        }
    }
}