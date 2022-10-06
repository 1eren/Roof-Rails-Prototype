using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveController : MonoBehaviour
{
	[SerializeField]private Camera orthoCamera;

	private Vector3 dif;
	private Vector3 rotationDif;
	private Vector3 firstPos;
	private Vector3 mousePos;
	private Vector3 rotateFirstPos;

	[HideInInspector] public bool isTouching;

	private float rotationLerpTime = 12f;
	private void OnEnable()
	{
		if (LevelManager.Instance == null)
			return;
		InputManager.Instance.OnTapUp.AddListener(() =>
		{
			dif = Vector3.zero;
			isTouching = false;
		});
		InputManager.Instance.OnTapDown.AddListener(GetFirstPos);
	}

	private void OnDisable()
	{
		if (LevelManager.Instance == null)
			return;
		InputManager.Instance.OnTapUp.RemoveListener(() =>
		{
			dif = Vector3.zero;
			isTouching = false;
		});
		InputManager.Instance.OnTapDown.RemoveListener(GetFirstPos);
	}
	private void Update()
	{
		firstPos = Vector3.Lerp(firstPos, mousePos, 20f * Time.deltaTime);

		rotateFirstPos = Vector3.Lerp(rotateFirstPos, mousePos, rotationLerpTime * Time.deltaTime);

		if (isTouching)
		{
			mousePos = orthoCamera.ScreenToWorldPoint(Input.mousePosition);
			dif = mousePos - firstPos;
		}
		rotationDif = mousePos - rotateFirstPos;
	}
	private void GetFirstPos()
	{
		isTouching = true;
		mousePos = orthoCamera.ScreenToWorldPoint(Input.mousePosition);
		firstPos = mousePos;
		rotateFirstPos = mousePos;
	}

	public Vector3 GetDirection()
	{
		return dif;
	}
	public Vector3 GetRotation()
	{
		return rotationDif;
	}
}
