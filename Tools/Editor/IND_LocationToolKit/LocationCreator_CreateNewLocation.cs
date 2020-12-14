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
    public static class LocationCreator_CreateNewLocation
    {
        public static void NewLocationSetup(string locationName, LocationCreator window)
        {
            string LocationRootPath = "Assets/" + StaticProjectSettings.projectName + "/Resources/Locations";
            string guid = AssetDatabase.CreateFolder("Assets/" + StaticProjectSettings.projectName + "/Scenes/Locations", locationName);
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

            string locationObjectPathGuid = AssetDatabase.CreateFolder("Assets/" + StaticProjectSettings.projectName + "/Resources/Locations", locationName);
            string locationObjectPath = AssetDatabase.GUIDToAssetPath(locationObjectPathGuid);
            Location locationAsset = window.CreateScriptableObjectLocation();
            locationAsset.locationName = locationName;
            if (window.markAsTestLocation)
            {
                locationAsset.isTestLocation = true;
            }
            AssetDatabase.CreateAsset(locationAsset, "Assets/" + StaticProjectSettings.projectName + "/Resources/Locations/" + locationName + "/" + locationName + ".Asset");

            string locationPath = "Assets/" + StaticProjectSettings.projectName + "/Resources/Locations/" + locationName + "/";
            AssetDatabase.SaveAssets();
            AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(locationAsset), locationObjectPath);
            //Create Settings Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Settings", newFolderPath);
            //Create Gameplay Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Gameplay", newFolderPath);
            //Create Ground Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Ground", newFolderPath);
            //Create Props Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Props", newFolderPath);
            //Create Buidlings Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Buildings", newFolderPath);
            //Create Lighting Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Lighting", newFolderPath);
            //Create Editor Scene
            LocationCreator_CreateScene.CreateLocationScene(locationName, "_Editor", newFolderPath);
            AssetDatabase.SaveAssets();
            //      locationInterface.locationsList.locations.Add(locationAsset);

            AddNewLocationToList(locationAsset.locationName, window);
            window.selectedLocation = locationAsset.locationName;
            window.LoadSelectedLocation();

            CreateLocationDataInScene(locationName, locationAsset, window);
        }

        public static void CreateLocationDataInScene(string sceneName,Location locationData, LocationCreator creator)
        {
            GameObject geo = new GameObject();
            geo.name = "Location Data";
            LocationHandler locationHandler = geo.AddComponent<LocationHandler>();
            locationHandler.locationData = locationData;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SceneManager.MoveGameObjectToScene(geo, SceneManager.GetSceneByName(sceneName + "_Settings"));

            EditorSceneManager.SaveScene(SceneManager.GetSceneByName(sceneName + "_Settings"));

            creator.currentActiveLocation = locationHandler.locationData;
            LocationCreator_AddDataToLevel.CreateLevelManager(sceneName + "_Settings", locationData);
            LocationCreator_AddDataToLevel.CreateInputManager(sceneName + "_Settings");
            LocationCreator_AddDataToLevel.CreateDefaultLighting(sceneName + "_Lighting");
        }

        public static void AddNewLocationToList(string locationName, LocationCreator window)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            LocationList locationList = Resources.Load("Locations/Location List") as LocationList;
            string LocationRootPath = "Locations";
            string fullpath = LocationRootPath + "/" + locationName + "/" + locationName;
            Location locationData = Resources.Load(fullpath) as Location;
            window.locationsList.locations.Add(locationData);
            AssetDatabase.SaveAssets();
            window.RefreshLocationsList();
        }
    }
}