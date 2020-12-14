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
    public static class LocationCreator_LoadLocation
    {
        public static void LoadSelectedLocation(LocationCreator window)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            string selectedLocation = window.selectedLocation;
            string LocationRootPath = "Assets/" + StaticProjectSettings.projectName + "/Scenes/Locations/";
            string fullpath = LocationRootPath + selectedLocation + "/" + selectedLocation;
            //Load Master Scene
            if (window.includeMasterSceneWhenOpening == true)
            {
                EditorSceneManager.OpenScene("Assets/" + StaticProjectSettings.projectName + "/Scenes/Master/MasterScene.unity", OpenSceneMode.Single);
            }

            if (window.includeMasterSceneWhenOpening == true)
            {
                //Load Settings File
                EditorSceneManager.OpenScene(fullpath + "_Settings" + ".unity", OpenSceneMode.Additive);
            }
            else
            {
                EditorSceneManager.OpenScene(fullpath + "_Settings" + ".unity", OpenSceneMode.Single);
            }
            //Load Gameplay Scene
            EditorSceneManager.OpenScene(fullpath + "_Gameplay" + ".unity", OpenSceneMode.Additive);
            //Load Ground Scene
            EditorSceneManager.OpenScene(fullpath + "_Ground" + ".unity", OpenSceneMode.Additive);
            //Load Props Scene
            EditorSceneManager.OpenScene(fullpath + "_Props" + ".unity", OpenSceneMode.Additive);
            //Load Buildings Scene
            EditorSceneManager.OpenScene(fullpath + "_Buildings" + ".unity", OpenSceneMode.Additive);
            //Load Lighting Scene
            EditorSceneManager.OpenScene(fullpath + "_Lighting" + ".unity", OpenSceneMode.Additive);
            //Load Editor Scene
            EditorSceneManager.OpenScene(fullpath + "_Editor" + ".unity", OpenSceneMode.Additive);

            LoadCurrentLocationData(window);

            if (window.includeRevelevantLevelTypeDataSceneWhenOpening == true)
            {
                window.OpenReleventLevelTypeDataScene();
            }
        }

        public static void LoadCurrentLocationData(LocationCreator window)
        {
            if (Object.FindObjectOfType<LocationHandler>() == null)
                return;

            window.currentActiveLocation = Object.FindObjectOfType<LocationHandler>().locationData;
        }
    }
}