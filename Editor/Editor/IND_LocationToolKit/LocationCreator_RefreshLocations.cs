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
    public static class LocationCreator_RefreshLocations
    {
        [Button("Refresh Locations List", ButtonSizes.Medium)]
        public static void RefreshLocationsList(LocationCreator locationInterface)
        {
            locationInterface.locationsList = Resources.Load("Locations/Location List") as LocationList;
            locationInterface.locationsList.locations.Clear();
            Location[] arrayOfLocations = GetAllInstances<Location>();
            foreach (Location item in arrayOfLocations)
            {
                locationInterface.locationsList.locations.Add(item);
            }

            if (locationInterface.includeTestLocations == false)
            {
                for (int i = 0; i < locationInterface.locationsList.locations.Count; i++)
                {
                    if (locationInterface.locationsList.locations[i].isTestLocation == false)
                    {
                        locationInterface.availableLocations.Add(locationInterface.locationsList.locations[i].locationName);
                    }
                }
            }
            else
            {
                for (int i = 0; i < locationInterface.locationsList.locations.Count; i++)
                {
                    locationInterface.availableLocations.Add(locationInterface.locationsList.locations[i].locationName);
                }
            }

            if (locationInterface.selectedLocation == null)
            {
                locationInterface.selectedLocation = locationInterface.locationsList.locations[0].locationName;
            }

            if (Object.FindObjectOfType<LocationHandler>() != null)
            {
                locationInterface.currentActiveLocation = Object.FindObjectOfType<LocationHandler>().locationData;

              
            }
            if (locationInterface.currentActiveLocation == null)
            {
                if (Object.FindObjectOfType<LevelManager>() != null)
                {
                    locationInterface.currentActiveLocation = Object.FindObjectOfType<LevelManager>().locationData;
                }
                else
                {
                    Debug.Log("Scene Does Not Contain Level Manager or Location data");
                }
            }
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

    }
}