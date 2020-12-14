using UnityEngine;
using UnityEditor;


namespace IND.DevTools
{
    public class IND_LoadResourceInspector
    {
        [MenuItem("IND Tools/Dev Settings", false, 1)]
        public static void LoadDevDebugSettings()
        {
            InspectTarget("DevSettings/DevSettings");
        }

        [MenuItem("IND Tools/Load Camera Settings", false, 99)]
        public static void LoadCameraSettings()
        {
            InspectTarget("Camera/CameraSettings_Default");
        }

        [MenuItem("IND Tools/Load Resource/Load Player Inventroy Dev", false, 100)]
        public static void LoadPlayerInventoryDev()
        {
            InspectTarget("Player/Inventory/Player Inventory Dev");
        }

        #region Load Inspector with Resource Functionality

        public static void InspectTarget(string directory)
        {
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            var inspectorInstance = ScriptableObject.CreateInstance(inspectorType) as EditorWindow;
            inspectorInstance.Show();
            var prevSelection = Selection.activeGameObject;
            Selection.activeObject = Resources.Load(directory);
            var isLocked = inspectorType.GetProperty("isLocked", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            isLocked.GetSetMethod().Invoke(inspectorInstance, new object[] { true });
            Selection.activeGameObject = prevSelection;

        }
        #endregion
    }
}
