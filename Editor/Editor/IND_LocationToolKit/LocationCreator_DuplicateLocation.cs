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
    public static class LocationCreator_DuplicateLocation
    {
        public static void DuplicateLocation(LocationCreator window)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();


            bool isEmpty = window.CheckIfNewLocationNameIsEmpty();
            if (isEmpty == true)
            {
                window.SendLocationStringIsMissingMessage();
                return;
            }

            bool isExisting = window.CheckIfLocationExists();
            if (isExisting == true)
            {
                window.SendMessageLocationAlreadyExists();
                return;
            }

            string LocationRootPath = "Assets/" + StaticProjectSettings.projectName + "/Scenes/Locations/";
            string guid = AssetDatabase.CreateFolder("Assets/" + StaticProjectSettings.projectName + "/Scenes/Locations", window.newLocationName);
            string locationObjectPathGuid = AssetDatabase.CreateFolder("Assets/" + StaticProjectSettings.projectName + "/Resources/Locations", window.newLocationName);
            string locationObjectPath = AssetDatabase.GUIDToAssetPath(locationObjectPathGuid);
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

            //Copy Settings Files
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Settings.unity", newFolderPath);
            //Copy Gameplay File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Gameplay.unity", newFolderPath);
            //Copy Ground File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Ground.unity", newFolderPath);
            //Copy Props File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Props.unity", newFolderPath);
            //Copy Buildings File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Buildings.unity", newFolderPath);
            //Copy Lighting File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Lighting.unity", newFolderPath);
            //Copy Editor File
            window.CopySceneFile(LocationRootPath + window.selectedLocation + "/" + window.selectedLocation, "_Editor.unity", newFolderPath);
            //Create Location Object
            Location locationAsset = ScriptableObject.CreateInstance<Location>();
            locationAsset.locationName = window.newLocationName;

            AssetDatabase.CreateAsset(locationAsset, locationObjectPath + "/" + window.newLocationName + ".Asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(locationAsset), newFolderPath);
            AssetDatabase.Refresh();

            LocationHandler locationHandle = Object.FindObjectOfType<LocationHandler>();
            locationHandle.locationData = locationAsset;

            window.RefreshLocationsList();
        }
    }
}