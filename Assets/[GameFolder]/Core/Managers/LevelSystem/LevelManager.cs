using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class LevelManager : Singleton<LevelManager>
{
    [HideInInspector]
    public UnityEvent OnLevelStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelFinish = new UnityEvent();

    bool isLevelStarted;
    [ReadOnly]
    [ShowInInspector]
    public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }

    private void Update()
    {
        if (!IsLevelStarted && Input.GetMouseButtonUp(0))
            StartLevel();
    }
    [Button]
    public void ReloadLevel()
    {
        FinishLevel();
    }

    public void LoadLastLevel()
    {
        FinishLevel();
    }

    public void StartLevel()
    {
        if (IsLevelStarted)
            return;
        IsLevelStarted = true;
        OnLevelStart.Invoke();
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted)
            return;
        IsLevelStarted = false;
        OnLevelFinish.Invoke();
    }
}
