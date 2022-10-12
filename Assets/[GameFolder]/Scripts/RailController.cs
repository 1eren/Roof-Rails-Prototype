using Sirenix.OdinInspector;
using UnityEngine;

public class RailController : MonoBehaviour
{
	[OnValueChanged("SetSticksAttributes")]
	[Range(0, 10)] public float distanceBetween = 1f;
	[OnValueChanged("SetSticksAttributes")] 
	[Range(2, 30)] public float scale = 1f;

	[SerializeField] public float speedIncrease;
	public bool isFinishStick;

	private bool isTriggered;

	//distance and scale will be updated instantly when the distance& scale changes on inspector
	private void SetSticksAttributes()
	{
		foreach (Transform item in transform)
		{
			if (item.GetSiblingIndex() % 2 == 0)
				item.localPosition = new Vector3(distanceBetween, item.localPosition.y, item.localPosition.z);
			else
				item.localPosition = new Vector3(-distanceBetween, item.localPosition.y, item.localPosition.z);

			item.localScale = new Vector3(scale, item.localScale.y, item.localScale.z);

			BoxCollider coll = GetComponent<BoxCollider>();
			coll.size = new Vector3(10 + scale, coll.size.y, scale);
		}
	}
	private void OnEnable()
	{
		GameManager.Instance.FallEvent.AddListener(ChangeColliderStatus);

		GameManager.Instance.JumpToFinish.AddListener(ChangeColliderStatus);
		GameManager.Instance.WinEvent.AddListener(ChangeColliderStatus);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;

		GameManager.Instance.FallEvent.RemoveListener(ChangeColliderStatus);

		GameManager.Instance.JumpToFinish.RemoveListener(ChangeColliderStatus);
		GameManager.Instance.WinEvent.RemoveListener(ChangeColliderStatus);
	}
	private void CheckStickSize(Transform playerT)
	{
		var stick = playerT.GetComponentInChildren<PlayerStickController>();
		float playerPosX = playerT.position.x;

		if (stick.StickSize < distanceBetween * 2)
		{
			CheckFinishStick();
			return;
		}

		if (playerPosX > transform.position.x + distanceBetween
			|| playerPosX < transform.position.x - distanceBetween)
		{
			CheckFinishStick();
			return;
		}
		EventManager.OnEnteredRail.Invoke(this);

	}
	private void CheckFinishStick()
	{
		if (!isFinishStick)
			GameManager.Instance.FallEvent.Invoke();
		else
			GameManager.Instance.JumpToFinish.Invoke();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController player))
		{
			if (player.isDeath && isTriggered)
				return;
			isTriggered = true;
			CheckStickSize(player.transform);
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController player))
		{
			if (player.isDeath)
				return;

			EventManager.OnExitRail.Invoke(this);
		}
	}

	private void ChangeColliderStatus()
	{
		foreach (var item in GetComponentsInChildren<Collider>())
			item.enabled = !item.enabled;
	}
}
