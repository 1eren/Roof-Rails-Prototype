using Dreamteck.Splines;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour, IInputListener
{

	[Header("References")]
	public InputManager inputManager;
	public SplineFollower splineFollower;
	public Transform mainBody;
	public float horizontalSpeed = 1f, forwardSpeed = 5f;

	public float xClampMin, xClampMax;

	private float xClampMinInit, xClampMaxInit;
	[ReadOnly] public bool isControllable = true;
	private void OnEnable()
	{
		Initialize();

		LevelManager.Instance.LevelStartEvent.AddListener(OnLevelStarted);
		LevelManager.Instance.LevelFinishEvent.AddListener(OnLevelFinished);
		EventManager.OnEnteredRail.AddListener(MoveOnRail);
		EventManager.OnExitRail.AddListener(ResetClamp);
	}
	private void OnDisable()
	{
		LevelManager.Instance.LevelStartEvent.RemoveListener(OnLevelStarted);
		LevelManager.Instance.LevelFinishEvent.RemoveListener(OnLevelFinished);
		EventManager.OnEnteredRail.RemoveListener(MoveOnRail);
		EventManager.OnExitRail.RemoveListener(ResetClamp);

	}
	private void Initialize()
	{
		splineFollower.follow = false;
		xClampMinInit = xClampMin;
		xClampMaxInit = xClampMax;
	}
	private void Update()
	{
		if (inputManager.isDown && isControllable)
			OnSlide(inputManager.SendSlide());
		inputManager.Tick();
	}
	private void OnLevelStarted()
	{
		splineFollower.follow = true;
		splineFollower.followSpeed = forwardSpeed;
	}

	private void OnLevelFinished()
	{
		Debug.Log("sa");
		splineFollower.follow = false;
		isControllable = false;
	}
	public void MoveOnRail(RailController rail)
	{
		xClampMin = rail.transform.localPosition.x - rail.distanceBetween + 0.4f;
		xClampMax = rail.transform.localPosition.x + rail.distanceBetween - 0.4f;
	}
	public void ResetClamp(RailController rail)
	{
		xClampMin = xClampMinInit;
		xClampMax = xClampMaxInit;
	}
	public void OnSlide(SlideData data)
	{
		var newLocalPosition = mainBody.localPosition + Vector3.right * data.delta.x * horizontalSpeed * Time.deltaTime;
		newLocalPosition.x = Mathf.Clamp(newLocalPosition.x, xClampMin, xClampMax);
		mainBody.localPosition = newLocalPosition;
	}
}
