using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private GameObject _settingsMenu;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private TMP_Text _stackText;

    private bool _isSettingsMenuOpened = false;

    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);

        LevelManager.OnStartLevel += LevelManager_OnStartLevel;
        LevelManager.OnStartEndLevel += LevelManager_OnStartEndLevel;
        LevelManager.OnFinishEndLevel += LevelManager_OnFinishEndLevel;

        Player.OnGainStack += Player_OnGainStack;
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _backButton.onClick.RemoveListener(OnBackButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        LevelManager.OnStartLevel -= LevelManager_OnStartLevel;
        LevelManager.OnStartEndLevel -= LevelManager_OnStartEndLevel;
        LevelManager.OnFinishEndLevel -= LevelManager_OnFinishEndLevel;

        Player.OnGainStack -= Player_OnGainStack;
    }

    private void SetButtonsControl(bool active)
    {
        _settingsButton.interactable = active;
        _restartButton.interactable = active;
    }

    private void SetLevelText(int level)
    {
        _levelText.text = $"Level {level}";
    }

    private void SetStackText(int value)
    {
        _stackText.text = value.ToString();
    }

    private void OnSettingsButtonClicked()
    {
        _isSettingsMenuOpened = !_isSettingsMenuOpened;

        _settingsMenu.SetActive(_isSettingsMenuOpened);
    }

    private void OnBackButtonClicked()
    {
        GameManager.Instance.ExitGame();
    }

    private void OnRestartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void LevelManager_OnStartLevel()
    {
        SetLevelText(LevelManager.Instance.CurrentLevel);
        SetStackText(0);

        gameObject.SetActive(true);
    }

    private void LevelManager_OnStartEndLevel()
    {
        SetButtonsControl(false);
    }

    private void LevelManager_OnFinishEndLevel()
    {
        SetButtonsControl(true);
    }

    private void Player_OnGainStack(int value)
    {
        SetStackText(value);
    }
}
