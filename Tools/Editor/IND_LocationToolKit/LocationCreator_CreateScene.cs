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
    public static class LocationCreator_CreateScene
    {
        public static void CreateLocationScene(string locationName, string desiredSceneName, string folderPath)
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            newScene.name = locationName + desiredSceneName;
            string pathName = folderPath + "/" + locationName + desiredSceneName + ".unity";
            EditorSceneManager.SaveScene(newScene, pathName);
        }
    }
}