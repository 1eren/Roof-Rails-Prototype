using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStick : MonoBehaviour
{
	[OnValueChanged("ChangeColor")] public GameColor color;
	public float increasingScale = 0.1f;
	private bool isTriggered;
	
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out PlayerController player))
		{
			isTriggered = true;
			if (player.color != color)
				return;
			gameObject.SetActive(false);
			player.GetComponentInChildren<ISliceable>().IncreaseScale(increasingScale);
		}
	}
}
