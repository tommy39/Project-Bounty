using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Locations;
using UnityEngine.SceneManagement;


namespace IND.Core.Managers.Level
{

    public class LevelManager : IND_Mono
    {
        public LevelType levelType;
        [Required] [InlineEditor] public Location locationData;
        public List<IND_Mono> levelScriptsToExecutePostMain = new List<IND_Mono>();

        private void Start()
        {
            LoadMasterScene();
        }
        public override void Init()
        {
            for (int i = 0; i < levelScriptsToExecutePostMain.Count; i++)
            {
                levelScriptsToExecutePostMain[i].Init();
            }
        }

        public void LoadLevelType()
        {
            string name = "";

            switch (levelType)
            {
                case LevelType.GAMEPLAY:
                    name = "LevelType_Gameplay";
                    break;
                case LevelType.UI:
                    name = "LevelType_UI";
                    break;
                case LevelType.CINEMATIC:
                    name = "LevelType_Cinematic";
                    break;
                case LevelType.NULL:
                    return;
            }

            Camera cam = FindObjectOfType<Camera>();
            if (cam != null)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
                return;
            }

            SceneManager.LoadScene(name, LoadSceneMode.Additive);
            StartCoroutine(SetActiveScene(name));
        }

        public override void Tick()
        {
            for (int i = 0; i < levelScriptsToExecutePostMain.Count; i++)
            {
                levelScriptsToExecutePostMain[i].Tick();
            }
        }

        public override void FixedTick()
        {
            for (int i = 0; i < levelScriptsToExecutePostMain.Count; i++)
            {
                levelScriptsToExecutePostMain[i].FixedTick();
            }
        }

        public override void LateTick()
        {
            for (int i = 0; i < levelScriptsToExecutePostMain.Count; i++)
            {
                levelScriptsToExecutePostMain[i].LateTick();
            }
        }

#if Unity_Editor
        [Button]
        public void AddSupportingSceneDataToEditor()
        {
            string sceneName = "";

            switch (levelType)
            {
                case LevelType.GAMEPLAY:
                    sceneName = "LevelType_Gameplay";
                    break;
                case LevelType.UI:
                    sceneName = "LevelType_UI";
                    break;
                case LevelType.CINEMATIC:
                    sceneName = "LevelType_Cinematic";
                    break;
                case LevelType.NULL:
                    break;
            }
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_ProjectDirectory/Scenes/Level Types/" + sceneName + ".unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);
        }
#endif

        private IEnumerator SetActiveScene(string sceneName)
        {
            yield return 0;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        public static LevelManager singleton;

        public void OnDrawGizmosSelected()
        {
            switch (levelType)
            {
                case LevelType.GAMEPLAY:
                    LevelManagerGameplay gameplayManger = GetComponent<LevelManagerGameplay>();
                    if (gameplayManger == null)
                    {
                        gameplayManger = gameObject.AddComponent<LevelManagerGameplay>();
                        gameplayManger.locationData = locationData;
                        gameplayManger.levelType = LevelType.GAMEPLAY;
                        gameplayManger.locationData.levelType = levelType;
                        DestroyImmediate(this);
                    }
                    break;
                case LevelType.UI:
                    LevelManagerUI uiManager = GetComponent<LevelManagerUI>();
                    if (uiManager == null)
                    {
                        uiManager = gameObject.AddComponent<LevelManagerUI>();
                        uiManager.locationData = locationData;
                        uiManager.levelType = LevelType.UI;
                        uiManager.locationData.levelType = levelType;
                        DestroyImmediate(this);
                    }
                    break;
                case LevelType.CINEMATIC:
                    break;
                case LevelType.NULL:
                    break;
            }

            if (locationData.levelType != levelType)
            {
                locationData.levelType = levelType;
            }
        }
        public void LoadMasterScene()
        {
            StartCoroutine(LoadMasterSceneOnNextFrame());
        }
        public IEnumerator LoadMasterSceneOnNextFrame()
        {
            yield return new WaitForEndOfFrame();
            LevelManager levelManager = Object.FindObjectOfType<LevelManager>();
            levelManager.LoadLevelType();
            yield return 1;
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                SceneManager.LoadScene("MasterScene", LoadSceneMode.Additive);
            }
        }
    }
}
