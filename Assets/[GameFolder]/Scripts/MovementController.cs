using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	private SplineFollower follower;
	public SplineFollower Follower => follower == null ? follower = GetComponentInParent<SplineFollower>() : follower;

	private SwerveController swerve;

	private float offSetX;
	public float clampX =0.3f;
	[InlineEditor]
	public MovementData moveData;

	private bool isGameStarted;
	
	private void OnEnable()
	{
		swerve = FindObjectOfType<SwerveController>();
		Follower.followSpeed = 0f;
	
		LevelManager.Instance.LevelStartEvent.AddListener(() => {
			Follower.followSpeed = moveData.speed;
			isGameStarted = true;
		});
		GameManager.Instance.WinEvent.AddListener(StopMovement);
		GameManager.Instance.FallEvent.AddListener(StopMovement);
	}
	private void OnDisable()
	{
		LevelManager.Instance.LevelStartEvent.RemoveListener(() =>
		{
			Follower.followSpeed = moveData.speed;
			isGameStarted = true;
		});
		GameManager.Instance.WinEvent.RemoveListener(StopMovement);
		GameManager.Instance.FallEvent.RemoveListener( () => Follower.follow = false);
	}
	private void Update()
	{
		if (!isGameStarted)
			return;
		
		offSetX = Follower.motion.offset.x;
		offSetX += swerve.GetDirection().x * moveData.swerveSpeed;
		offSetX = Mathf.Clamp(offSetX, -clampX, clampX);
		Follower.motion.offset = new Vector2(offSetX, Follower.motion.offset.y);
	}
	private void StopMovement()
	{
		Follower.follow = false;
	}
}
