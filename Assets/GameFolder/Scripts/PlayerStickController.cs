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
		transform.DOMoveZ(2, .2f).SetSpeedBased(true).SetDelay(1f);
	}

	[Button]
	public void ResetScale()
	{
		transform.position = new Vector3(0, 1, 0);
		transform.localScale = new Vector3(2, 1, 1);
	}

	[Button]
	public void IncreaseScale(float amount, Vector3 direction)
	{
		transform.position += direction * amount / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
		transform.localScale += direction * amount; // Scale object in the specified direction
	}
	[Button]
	public void DecreaseScale(float amount, Vector3 direction)
	{
		Debug.Log(direction.x);
		Debug.Log(transform.position.x);


		amount = (transform.localScale.x / 2) - Mathf.Abs(direction.x - transform.position.x);

		if (direction.x > 0)
			direction = Vector3.right;
		else
			direction = Vector3.left;

		Debug.Log(amount);
		if (transform.localScale.x - amount < 0.1f)
		{
			transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
			return;
		}




		Debug.Log(transform.localScale + "  1");
		transform.localScale -= Vector3.right * amount;
		Debug.Log(transform.localScale + "  2");

		Debug.Log(transform.position + "  1");
		transform.position -= direction * amount / 2;
		Debug.Log(transform.position + "  2");
	}
}
