using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Slicer : MonoBehaviour
{
	private bool isTriggered;
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out ISliceable sliceable))
		{
			GetComponent<Collider>().enabled = false;
			isTriggered = true;
			sliceable.DecreaseScale(transform.position);
		}
	}
}
