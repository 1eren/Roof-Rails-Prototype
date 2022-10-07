using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;

public class PlayerStickController : MonoBehaviour, ISliceable, IThrowable
{
	private Tween resetPositionTween;
	[SerializeField] private float minimumStickScale = 0.1f;
	public void IncreaseScale(float amount)
	{
		transform.localScale += amount * Vector3.right;
	}
	public void DecreaseScale(Vector3 direction)
	{
		//finding difference and required scale for slicing
		float amount = (transform.localScale.x / 2) - Mathf.Abs(direction.x - transform.position.x);

		if (transform.localScale.x - amount < minimumStickScale)//if the stick will be very small we stopping at a small value
		{
			transform.localScale = new Vector3(minimumStickScale, transform.localScale.y, transform.localScale.z);
			return;
		}
		//find out if the obstacle is on the right or left 
		Vector3 roundedValue = direction.x > transform.position.x ? Vector3.right : Vector3.left;

		transform.localScale -= Vector3.right * amount;
		transform.position -= roundedValue * amount / 2;
		CreateNewPart(amount, new Vector3(direction.x, transform.position.y, transform.position.z));

	}

	public void CreateNewPart(float xScale, Vector3 hitPoint)
	{
		if (resetPositionTween != null)//kill tween if this function triggered twice
			resetPositionTween.Kill();

		Vector3 creatingPos = hitPoint;

		creatingPos += (hitPoint.x > transform.position.x ? Vector3.right * xScale / 2 : Vector3.left * xScale / 2);//set object  position by cutting pos
		GameObject newPart = PoolingSystem.Instance.InstantiateAPS("Stick", creatingPos);
		newPart.transform.localScale = new Vector3(xScale, newPart.transform.localScale.y, newPart.transform.localScale.z);

		//reset position after small delay
		resetPositionTween = transform.DOLocalMoveX(0, .5f).SetDelay(.5f);
		newPart.GetComponent<IThrowable>().Throw(Vector3.Normalize(hitPoint - transform.position + Vector3.up) * 2f);
	}

	public void Throw(Vector3 force)
	{
		throw new NotImplementedException();
	}
}
