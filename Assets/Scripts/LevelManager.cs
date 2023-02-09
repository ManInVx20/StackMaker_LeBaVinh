using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static event Action OnStartLevel;
    public static event Action OnFinishLevel;
    public static event Action OnStartEndLevel;
    public static event Action OnFinishEndLevel;

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;
    [SerializeField]
    private Transform _roadStacksHolder;
    [SerializeField]
    private Transform _bridgeStacksHolder;

    private int _currentLevel;
    public int CurrentLevel => _currentLevel;

    public Vector3 StartPosition => _startPoint.position;
    public Vector3 EndPoint => _endPoint.position;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        foreach (var stack in _roadStacksHolder.GetComponentsInChildren<Stack>())
        {
            stack.Setup(null);
        }

        foreach (var stack in _bridgeStacksHolder.GetComponentsInChildren<Stack>())
        {
            stack.Setup(null);
        }

        _currentLevel = PlayerPrefs.GetInt("Level", 1);

        OnStartLevel?.Invoke();
    }

    public void FinishLevel()
    {
        OnFinishLevel?.Invoke();
    }

    public void StartEndLevel()
    {
        OnStartEndLevel?.Invoke();

        StartCoroutine(FinishEndLevelCoroutineCoroutine(2.0f));
    }

    private IEnumerator FinishEndLevelCoroutineCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        OnFinishEndLevel?.Invoke();
    }
}
