using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class GameManager : Singleton<GameManager>
{
	[HideInInspector] public UnityEvent FallEvent = new UnityEvent();
	[HideInInspector] public UnityEvent DeathEvent = new UnityEvent();
	[HideInInspector] public UnityEvent WinEvent = new UnityEvent();
}
