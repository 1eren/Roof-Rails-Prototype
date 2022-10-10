using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStick : MonoBehaviour
{
	public float increasingScale = 0.1f;
	private bool isTriggered;
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out PlayerController player))
		{
			gameObject.SetActive(false);
			isTriggered = true;
			player.GetComponentInChildren<ISliceable>().IncreaseScale(increasingScale);
		}
	}
}
