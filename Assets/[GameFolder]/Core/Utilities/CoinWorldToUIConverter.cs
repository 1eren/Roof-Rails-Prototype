using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class CoinWorldToUIConverter : MonoBehaviour
{
	public Transform GemImageTarget;
	public Canvas Canvas;

	private const float GEM_MOVEMENT_DURATION = 0.65f;

	private void OnEnable()
	{
		EventManager.GemCollected.AddListener(OnGemCollected);
	}
	private void OnDisable()
	{
		EventManager.GemCollected.RemoveListener(OnGemCollected);
	}
	private void OnGemCollected(Vector3 gemPosition)
	{
		Vector3 gemUIPosition = WorldToUISpace(Canvas, gemPosition);
		GameObject gemImageGo = PoolingSystem.Instance.InstantiateAPS("CoinImage", gemUIPosition);
		gemImageGo.transform.SetParent(GemImageTarget.transform);
		gemImageGo.transform.DOMove(GemImageTarget.transform.position, GEM_MOVEMENT_DURATION).SetEase(Ease.InCirc)
		.OnComplete(() =>
		{
			PoolingSystem.Instance.DestroyAPS(gemImageGo);
			PlayerPrefs.SetInt(PlayerPrefKeys.COIN, PlayerPrefs.GetInt(PlayerPrefKeys.COIN) + 1);

			GameManager.Instance.PlayerPrefsUptated.Invoke();

		})
		.OnKill(() =>
		{
			PoolingSystem.Instance.DestroyAPS(gemImageGo);
		});
	}
	private Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPosition)
	{
		//Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

		//Convert the screenpoint to ui rectangle local point
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPoint);

		//Convert the local point to world point
		return canvas.transform.TransformPoint(localPoint);
	}
}