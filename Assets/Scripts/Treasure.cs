using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private int _idleHash;
    private int _openHash;

    private void Awake()
    {
        _idleHash = Animator.StringToHash("Idle");
        _openHash = Animator.StringToHash("Open");
    }

    private void OnEnable()
    {
        LevelManager.OnStartEndLevel += LevelManager_OnStartEndLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnStartEndLevel -= LevelManager_OnStartEndLevel;
    }

    private void LevelManager_OnStartEndLevel()
    {
        _animator.SetTrigger(_openHash);
    }
}
