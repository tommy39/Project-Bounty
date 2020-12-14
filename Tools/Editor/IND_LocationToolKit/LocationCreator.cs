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

namespace IND.DevTools.Locations
{
    public class LocationCreator : OdinEditorWindow
    {
        [HideInInspector] public ValueDropdownList<string> availableLocations = new ValueDropdownList<string>();

        [SerializeField] [InlineEditor] public LocationList locationsList;

        [InlineEditor] public Location currentActiveLocation;

        [ValueDropdown("availableLocations")]
        public string selectedLocation;
        public bool includeMasterSceneWhenOpening = true;
        public bool includeRevelevantLevelTypeDataSceneWhenOpening = true;

        [FoldoutGroup("Create new Location")] public string newLocationName;
        [FoldoutGroup("Create new Location")] public bool markAsTestLocation = true;
        [FoldoutGroup("Create new Location")] public bool includeTestLocations = true;

        public static LocationCreator window;

        [MenuItem("IND Tools/Location Creator", false, 3)]
        private static void OpenLocationInteface()
        {
            window = GetWindow<LocationCreator>();

            //Load Locations List
            window.locationsList = Resources.Load("Locations/Location List") as LocationList;

            foreach (LocationList item in Resources.FindObjectsOfTypeAll(typeof(LocationList)) as LocationList[])
            {
                window.locationsList = item;
            }
            if (window.locationsList != null)
            {
                window.RefreshLocationsList();
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.RefreshLocationsList();
        }

        LocationCreator GetRefreshedWindowReference()
        {
            window = GetWindow<LocationCreator>();

            //Load Locations List
            window.locationsList = Resources.Load("Locations/Location List") as LocationList;

            foreach (LocationList item in Resources.FindObjectsOfTypeAll(typeof(LocationList)) as LocationList[])
            {
                window.locationsList = item;
            }
            if (window.locationsList != null)
            {
                window.RefreshLocationsList();
            }

            window.RefreshLocationsList();

            return window;
        }


        [Button(ButtonSizes.Gigantic), GUIColor(0, 1, 0)]
        public void LoadSelectedLocation()
        {
            LocationCreator_LoadLocation.LoadSelectedLocation(GetRefreshedWindowReference());
        }

        [FoldoutGroup("Create new Location")]
        [Button("Create New Location", ButtonSizes.Large)]
        public void CreateLocation()
        {

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            bool isEmpty = CheckIfNewLocationNameIsEmpty();
            if (isEmpty == true)
            {
                SendLocationStringIsMissingMessage();
                return;
            }

            bool isExisting = CheckIfLocationExists();
            if (isExisting == true)
            {
                SendMessageLocationAlreadyExists();
                return;
            }
            LocationCreator_CreateNewLocation.NewLocationSetup(newLocationName, GetRefreshedWindowReference());
        }

        public Location CreateScriptableObjectLocation()
        {
            return ScriptableObject.CreateInstance<Location>();
        }

        [Button("Delete Selected Location", ButtonSizes.Medium)]
        public void DeleteLocation()
        {
            LocationCreator_DeleteSelectedLocation.DeleteLocation(GetRefreshedWindowReference());
        }

        [Button("Create Duplicate Of Selected Location", ButtonSizes.Medium)]
        public void DuplicateLocation()
        {
            LocationCreator_DuplicateLocation.DuplicateLocation(GetRefreshedWindowReference()) ;
        }

        public void CopySceneFile(string path, string endingName, string newFolderPath)
        {
            FileUtil.CopyFileOrDirectory(path + endingName, newFolderPath + "/" + newLocationName + endingName);
        }

        public bool CheckIfNewLocationNameIsEmpty()
        {
            bool isEmpty = true;
            if (string.IsNullOrEmpty(newLocationName))
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            return isEmpty;
        }

        public bool CheckIfLocationExists()
        {
            bool doesExist = false;
            for (int i = 0; i < availableLocations.Count; i++)
            {
                if (newLocationName.Equals(selectedLocation))
                {
                    doesExist = true;
                }
                //                break;
            }
            return doesExist;
        }

        public void SendLocationStringIsMissingMessage()
        {
            EditorUtility.DisplayDialog("Function Failed", "New Location String Cannot be Empty", "Ok", "");
        }
        public void SendMessageLocationAlreadyExists()
        {
            EditorUtility.DisplayDialog("Function Failed", "New Location String Already Exists", "Ok", "");
        }

        [Button(ButtonSizes.Large), GUIColor(0.431f, 0.549f, 0.102f)]
        public static void AddMasterSceneToCurrentScenes()
        {
            EditorSceneManager.OpenScene("Assets/" + StaticProjectSettings.projectName + "/Scenes/Master/MasterScene.unity", OpenSceneMode.Additive);
        }

        [Button(ButtonSizes.Large), GUIColor(0.83f, 0.4313726f, 0.01960784f)]
        public static void RemoveMasterSceneFromCurrentScenes()
        {
            EditorSceneManager.CloseScene(SceneManager.GetSceneByName("MasterScene"), true);
        }

        [Button("Refresh Locations List", ButtonSizes.Medium)]
        public void RefreshLocationsList()
        {
            LocationCreator_RefreshLocations.RefreshLocationsList(window);
        }
        [Button("Open Relevent Level Type Data Scene", ButtonSizes.Medium)]
        public void OpenReleventLevelTypeDataScene()
        {
            Object.FindObjectOfType<LevelManager>().AddSupportingSceneDataToEditor();
        }
    }
}