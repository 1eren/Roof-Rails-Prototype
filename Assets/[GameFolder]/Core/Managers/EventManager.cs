using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
	public static GemCollectEvent OnGemCollected = new GemCollectEvent();

	public static UnityEvent<RailController> OnEnteredRail = new UnityEvent<RailController>();
	public static UnityEvent<RailController> OnExitRail = new UnityEvent<RailController>();
}
public class GemCollectEvent : UnityEvent<Vector3> { }