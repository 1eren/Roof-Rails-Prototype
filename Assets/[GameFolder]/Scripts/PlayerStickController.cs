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


	public void IncreaseScale(float amount)
	{
		transform.localScale += amount * Vector3.right;
	}
	public void DecreaseScale(Vector3 direction)
	{

		float amount = (transform.localScale.x / 2) - Mathf.Abs(direction.x - transform.position.x);

		CreateNewPart(amount, new Vector3(direction.x, transform.position.y, transform.position.z));

		//if the stick will be very small we stopping at a small value
		if (transform.localScale.x - amount < 0.1f)
		{
			transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
			return;
		}

		Vector3 roundedValue = direction.x > 0 ? Vector3.right : Vector3.left;
		transform.localScale -= Vector3.right * amount;
		transform.position -= roundedValue * amount / 2;
	}

	public void CreateNewPart(float xScale, Vector3 hitPoint)
	{
		Vector3 creatingPos = hitPoint;
		creatingPos += (hitPoint.x > 0 ? Vector3.right * xScale / 2 : Vector3.left * xScale / 2);//set object  position by cutting pos
		GameObject newPart = PoolingSystem.Instance.InstantiateAPS("Stick", creatingPos);
		newPart.transform.localScale = new Vector3(xScale, newPart.transform.localScale.y, newPart.transform.localScale.z);
		//newPart.GetComponent<IThrowable>().Throw(1, hitPoint);
	}
}
