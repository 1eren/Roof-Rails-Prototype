using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

public class MovementController : MonoBehaviour, IInputListener
{

	[Header("References")]
	public InputManager inputManager;
	public SplineFollower splineFollower;
	public Transform mainBody;
	public PlayerMoveData moveData;

	private float xClampMinInit, xClampMaxInit;
	[ReadOnly] public bool isControllable = true;
	private void OnEnable()
	{
		Initialize();

		LevelManager.Instance.LevelStartEvent.AddListener(OnLevelStarted);

		GameManager.Instance.FallEvent.AddListener(FallPlayer);
		GameManager.Instance.WinEvent.AddListener(StopPlayer);

		EventManager.OnEnteredRail.AddListener(MoveOnRail);
		EventManager.OnExitRail.AddListener(ResetClamp);
	}
	private void OnDisable()
	{
		LevelManager.Instance.LevelStartEvent.RemoveListener(OnLevelStarted);

		GameManager.Instance.FallEvent.RemoveListener(FallPlayer);
		GameManager.Instance.WinEvent.AddListener(StopPlayer);

		EventManager.OnEnteredRail.RemoveListener(MoveOnRail);
		EventManager.OnExitRail.RemoveListener(ResetClamp);

	}
	private void Initialize()
	{
		splineFollower.follow = false;
		xClampMinInit = moveData.xClampMin;
		xClampMaxInit = moveData.xClampMax;
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
		splineFollower.followSpeed = moveData.forwardSpeed;
	}

	private void FallPlayer()
	{
		Run.After(1, () => splineFollower.follow = false);
		isControllable = false;
	}
	private void StopPlayer()
	{
		splineFollower.follow = false;
		isControllable = false;
	}
	public void MoveOnRail(RailController rail)
	{
		splineFollower.followSpeed += rail.speedIncrease;
		xClampMinInit = rail.transform.position.x - rail.distanceBetween + 0.4f;//added 0.4 because player's localscale
		xClampMaxInit = rail.transform.position.x + rail.distanceBetween - 0.4f;
	}
	public void ResetClamp(RailController rail)
	{
		xClampMinInit = moveData.xClampMin;
		xClampMaxInit = moveData.xClampMax;
	}
	public void OnSlide(SlideMoveData data)
	{
		if (!isControllable)
			return;
		var newLocalPosition = mainBody.localPosition + Vector3.right * data.delta.x * moveData.horizontalSpeed * Time.deltaTime;
		newLocalPosition.x = Mathf.Clamp(newLocalPosition.x, xClampMinInit, xClampMaxInit);
		mainBody.localPosition = newLocalPosition;
	}
}
