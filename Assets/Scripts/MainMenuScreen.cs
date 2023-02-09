using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _exitButton;

    private void OnEnable()
    {
        _startButton?.onClick.AddListener(OnStartButtonClicked);
        _exitButton?.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _startButton?.onClick.RemoveListener(OnStartButtonClicked);
        _exitButton?.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void OnExitButtonClicked()
    {
        Debug.Log("Exit game.");
        Application.Quit();
    }
}
