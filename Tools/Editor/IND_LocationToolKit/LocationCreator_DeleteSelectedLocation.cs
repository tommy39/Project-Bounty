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
    public static class LocationCreator_DeleteSelectedLocation
    {
        public static void DeleteLocation(LocationCreator window)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            string fullpath = "Locations" + "/" + window.selectedLocation + "/" + window.selectedLocation;
            Location locationData = Resources.Load(fullpath) as Location;
            window.locationsList.locations.Remove(locationData);
            string SceneRootPath = "Assets/" + StaticProjectSettings.projectName + "/Scenes/Locations/";
            string LocationPath = "Assets/" + StaticProjectSettings.projectName + "/Resources/Locations/";
            for (int i = 0; i < window.availableLocations.Count; i++)
            {
                if (window.availableLocations[i].Text == window.selectedLocation)
                {
                    window.availableLocations.Remove(window.availableLocations[i]);
                }
            }
            FileUtil.DeleteFileOrDirectory(SceneRootPath + window.selectedLocation + "/");
            FileUtil.DeleteFileOrDirectory(LocationPath + window.selectedLocation + "/");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            window.selectedLocation = window.locationsList.locations[0].locationName;
            window.RefreshLocationsList();
        }
    }
}