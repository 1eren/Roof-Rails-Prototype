using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Slicer : MonoBehaviour
{
	[SerializeField] private bool isTriggered;
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out ISliceable sliceable))
		{
			isTriggered = true;
			sliceable.DecreaseScale(transform.position); 
		}
	}
}
