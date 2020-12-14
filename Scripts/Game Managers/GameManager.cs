using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IND.Core.Managers.Level;
using IND.Core.Inputs;
using IND.Core.Client;

namespace IND.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameStartState startState;
        public LevelManager currentLevelManager;
        private InputManager inputManager;
        private InputManagerResetter inputManagerResetter;
        private ClientManager clientManager;
        private GameStateManager gameStateManager;
        void Start()
        {

            switch (startState)
            {
                case GameStartState.PlayFromMainMenu:
                    break;
                case GameStartState.CurrentLevel:
                    break;
                default:
                    break;
            }

            currentLevelManager = FindObjectOfType<LevelManager>();
            inputManager = FindObjectOfType<InputManager>();
            clientManager = FindObjectOfType<ClientManager>();
            gameStateManager = FindObjectOfType<GameStateManager>();
            inputManagerResetter = FindObjectOfType<InputManagerResetter>();

            inputManager.Init();
            clientManager.Init();
            gameStateManager.Init();
            currentLevelManager.Init();
            inputManagerResetter.Init();
        }
        void Update()
        {
            inputManager.Tick();
            clientManager.Tick();
            gameStateManager.Tick();
            currentLevelManager.Tick();
            inputManagerResetter.Tick();
        }

        private void FixedUpdate()
        {
            inputManager.FixedTick();
            clientManager.FixedTick();
            gameStateManager.FixedTick();
            currentLevelManager.FixedTick();
            inputManagerResetter.FixedTick();
        }

        private void LateUpdate()
        {
            inputManager.LateTick();
            clientManager.LateTick();
            gameStateManager.LateTick();
            currentLevelManager.LateTick();
            inputManagerResetter.LateTick();
        }

        public void OnLocationChanged()
        {
            currentLevelManager = FindObjectOfType<LevelManager>();
            currentLevelManager.Init();
        }

        private IEnumerator ReEnable()
        {
            yield return 0;
            this.enabled = true;
            Start();
        }
    }
}