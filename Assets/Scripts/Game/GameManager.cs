using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private List<GameListeners.IGameListener> _gameListeners = new();

        private void Awake()
        {
            InitializeStates();
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            foreach (var state in _gameListeners)
            {
                if(state is GameListeners.IStartGameListener start)
                    start.OnStartGame();
            }
        }

        public void FinishGame()
        {
            foreach (var state in _gameListeners)
            {
                if(state is GameListeners.IFinishGameListener finish)
                    finish.OnFinishGame();
            }
        }

        private void InitializeStates()
        {
            _gameListeners = GetComponentsInChildren<GameListeners.IGameListener>().ToList();
        }
    }
}