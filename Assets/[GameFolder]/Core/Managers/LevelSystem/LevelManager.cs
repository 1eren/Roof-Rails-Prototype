using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-75)]
public class LevelManager : Singleton<LevelManager>
{
	[HideInInspector]
	public UnityEvent LevelStartEvent = new UnityEvent();
	[HideInInspector]
	public UnityEvent LevelFinishEvent = new UnityEvent();

	bool isLevelStarted;
	bool isLevelFinished;
	[ReadOnly]
	[ShowInInspector]
	public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }
	public bool IsLevelFinished { get { return isLevelFinished; } set { isLevelFinished = value; } }

	private void Update()
	{
		if (!IsLevelStarted && Input.GetMouseButtonUp(0))
			StartLevel();
	}
	[Button]
	public void ReloadLevel()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadLastLevel()
	{
		FinishLevel();
	}
	public void OnLevelStart()
	{
		LevelStartEvent?.Invoke();
	}
	public void OnFinishLevel()
	{
		LevelFinishEvent?.Invoke();
	}
	public void StartLevel()
	{
		if (IsLevelStarted)
			return;
		IsLevelStarted = true;
		OnLevelStart();
	}

	public void FinishLevel()
	{
		if (!IsLevelStarted)
			return;
		IsLevelStarted = false;
		OnFinishLevel();
	}
}
