using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Game
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int _maxLevel;

    public bool IsLastLevel => PlayerPrefs.GetInt("Level", 1) == _maxLevel;

    private GameState _currentState;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    public void ChangeGameState(GameState newState)
    {
        if (_currentState == newState)
        {
            return;
        }

        _currentState = newState;
    }

    public void StartGame()
    {
        ChangeGameState(GameState.Game);
        LoadGameScene();
    }

    public void NextGame()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        if (!IsLastLevel)
        {
            PlayerPrefs.SetInt("Level", currentLevel + 1);
        }

        LoadGameScene();
    }

    public void ExitGame()
    {
        ChangeGameState(GameState.MainMenu);
        LoadMainMenuScene();
    }

    private void LoadGameScene()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        SceneManager.LoadSceneAsync($"Level_{currentLevel}");
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
