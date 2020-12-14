using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using IND.Core.Managers.Level;
using IND.Core.Locations;
using IND.Core.Inputs;

namespace IND.DevTools.Locations
{
    public static class LocationCreator_AddDataToLevel
    {
        public static void CreateLevelManager(string sceneName, Location locationData)
        {
            GameObject geo = new GameObject();
            geo.name = "Level Manager";
            LevelManager levelManager = geo.AddComponent<LevelManager>();
            levelManager.locationData = locationData;
            SceneManager.MoveGameObjectToScene(geo, SceneManager.GetSceneByName(sceneName));
        }

        public static void CreateInputManager(string sceneName)
        {
            UnityEngine.Object pPrefab = (Object)AssetDatabase.LoadAssetAtPath("Assets/_ProjectDirectory/Prefabs/Input/Input_Manager.prefab", typeof(Object)); // note: not .prefab!
            PrefabUtility.InstantiatePrefab(pPrefab, SceneManager.GetSceneByName(sceneName));

            InputManager inputManager = Object.FindObjectOfType<InputManager>();

            LevelManager levelManager = Object.FindObjectOfType<LevelManager>();
           
        }

        public static void CreateDefaultLighting(string sceneName)
        {
            UnityEngine.Object pPrefab = (Object)AssetDatabase.LoadAssetAtPath("Assets/_ProjectDirectory/Prefabs/Locations/DefaulLightingSetup.prefab", typeof(Object)); // note: not .prefab!
            PrefabUtility.InstantiatePrefab(pPrefab, SceneManager.GetSceneByName(sceneName));
        }
    }
}