using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Slicer : MonoBehaviour
{
	[SerializeField] private bool isTriggered;
	//private void OnCollisionEnter(Collision collision)
	//{
	//	if (!isTriggered && collision.gameObject.TryGetComponent(out ISliceable sliceable))
	//	{
	//		GetComponent<Collider>().enabled = false;
	//		isTriggered = true;
	//		sliceable.DecreaseScale(transform.position);
	//	}
	//}
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
