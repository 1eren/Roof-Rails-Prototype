using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;

public class PlayerStickController : MonoBehaviour, ISliceable
{
	[SerializeField] private Material slicedMaterial;
	public Material SlicedMaterial => slicedMaterial;

	private void Start()
	{
		//transform.DOMoveZ(2, .2f).SetSpeedBased(true).SetDelay(1f);
	}

	[Button]
	public void ResetScale()
	{
		transform.position = new Vector3(0, 1, 0);
		transform.localScale = new Vector3(2, 1, 1);
	}

	[Button]
	public void IncreaseScale(float amount)
	{
		transform.localScale += amount * Vector3.right; // Scale object in the specified direction
	}
	[Button]
	public void DecreaseScale(Vector3 direction)
	{

		float amount = (transform.localScale.x / 2) - Mathf.Abs(direction.x - transform.position.x);

		//if the stick will be very small 
		if (transform.localScale.x - amount < 0.1f)
		{
			transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
			return;
		}

		direction = direction.x > 0 ? Vector3.right : Vector3.left;
		transform.localScale -= Vector3.right * amount;
		transform.position -= direction * amount / 2;
	}
}
