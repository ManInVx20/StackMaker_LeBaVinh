using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraState
{
    public Vector3 Offset;
    public Vector3 EulerAngles;
}

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 30.0f;
    [SerializeField]
    private float _turnSpeed = 90.0f;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private CameraState _startState;
    [SerializeField]
    private CameraState _finishState;
    [SerializeField]
    private CameraState _endState;

    private CameraState _currentState = default;

    private void OnEnable()
    {
        LevelManager.OnStartLevel += LevelManager_OnStartLevel;
        LevelManager.OnFinishLevel += LevelManager_OnFinishLevel;
        LevelManager.OnStartEndLevel += LevelManager_OnStartEndLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnStartLevel -= LevelManager_OnStartLevel;
        LevelManager.OnFinishLevel -= LevelManager_OnFinishLevel;
        LevelManager.OnStartEndLevel -= LevelManager_OnStartEndLevel;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _targetTransform.position + _currentState.Offset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(_currentState.EulerAngles);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void SwitchCameraState(CameraState newState)
    {
        if (!_currentState.Equals(default(CameraState)) && _currentState.Equals(newState))
        {
            return;
        }

        _currentState = newState;
    }

    private void LevelManager_OnStartLevel()
    {
        SwitchCameraState(_startState);
    }

    private void LevelManager_OnFinishLevel()
    {
        SwitchCameraState(_finishState);
    }

    private void LevelManager_OnStartEndLevel()
    {
        SwitchCameraState(_endState);
    }
}
