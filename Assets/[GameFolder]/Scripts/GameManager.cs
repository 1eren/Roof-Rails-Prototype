using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class GameManager : Singleton<GameManager>
{
	[HideInInspector] public UnityEvent PlayerFalled = new UnityEvent();

	[HideInInspector] public UnityEvent JumpedToFinish = new UnityEvent();

	[HideInInspector] public UnityEvent PlayerDied = new UnityEvent();
	[HideInInspector] public UnityEvent GameWinEvent = new UnityEvent();

	[HideInInspector] public UnityEvent PlayerPrefsUptated = new UnityEvent();

	private void Awake()
	{
		Application.targetFrameRate = 120;
	}
}
