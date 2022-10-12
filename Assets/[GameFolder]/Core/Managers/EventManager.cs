using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
	public static UnityEvent<Vector3> GemCollected = new UnityEvent<Vector3>();
	public static UnityEvent<CollectableStick> StickCollected = new UnityEvent<CollectableStick>();
	public static UnityEvent<float> StickDecrased = new UnityEvent<float>();
	public static UnityEvent<RailController> EnteredRail = new UnityEvent<RailController>();
	public static UnityEvent<RailController> ExitedRail = new UnityEvent<RailController>();
}
