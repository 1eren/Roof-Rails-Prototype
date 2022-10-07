using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
	private SplineFollower follower;
	public SplineFollower Follower => follower == null ? follower = GetComponent<SplineFollower>() : follower;

	private SwerveController swerve;

	private float offSetX;
	[InlineEditor]
	public MovementData moveData;
	private bool isGameStarted;

	private void Start()
	{
		swerve = FindObjectOfType<SwerveController>();
		Follower.followSpeed = 0f;
	}

	private void OnEnable()
	{
		if (LevelManager.Instance == null)
			return;
		LevelManager.Instance.OnLevelStart.AddListener(() => {
			Follower.followSpeed = moveData.speed;
			isGameStarted = true;
		});	
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null)
			return;
		LevelManager.Instance.OnLevelStart.RemoveListener(() => {
			Follower.followSpeed = moveData.speed;
			isGameStarted = true;
		});
	}
	private void Update()
	{
		if (!isGameStarted)
			return;
		
		offSetX = Follower.motion.offset.x;
		offSetX += swerve.GetDirection().x * moveData.swerveSpeed;
		offSetX = Mathf.Clamp(offSetX, -moveData.clampMovement, moveData.clampMovement);
		Follower.motion.offset = new Vector2(offSetX, Follower.motion.offset.y);
	}
}