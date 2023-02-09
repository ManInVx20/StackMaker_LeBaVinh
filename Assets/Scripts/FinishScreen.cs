using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _stackText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _nextButton;
    [SerializeField]
    private Button _backButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _nextButton.onClick.AddListener(OnNextButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
        _backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void Start()
    {
        LevelManager.OnFinishEndLevel += LevelManager_OnFinishEndLevel;
        Player.OnGainStack += Player_OnGainStack;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        LevelManager.OnFinishEndLevel -= LevelManager_OnFinishEndLevel;
        Player.OnGainStack -= Player_OnGainStack;
    }

    private void SetStackText(int value)
    {
        _stackText.text = $"{value} {(value == 0 ? "stack" : "stacks")}";
    }

    private void SetButtons(bool isLastLevel)
    {
        if (!isLastLevel)
        {
            _nextButton.gameObject.SetActive(true);
            _backButton.gameObject.SetActive(false);
        }
        else
        {
            _nextButton.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(true);
        }
    }

    private void OnRestartButtonClicked()
    {
        gameObject.SetActive(false);

        GameManager.Instance.StartGame();
    }

    private void OnNextButtonClicked()
    {
        GameManager.Instance.NextGame();
    }

    private void OnBackButtonClicked()
    {
        GameManager.Instance.ExitGame();
    }

    private void LevelManager_OnFinishEndLevel()
    {
        SetButtons(GameManager.Instance.IsLastLevel);

        gameObject.SetActive(true);
    }

    private void Player_OnGainStack(int value)
    {
        SetStackText(value);
    }
}
