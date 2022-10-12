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
		GameManager.Instance.PlayerFalled.AddListener(ChangeColliderStatus);

		GameManager.Instance.JumpedToFinish.AddListener(ChangeColliderStatus);
		GameManager.Instance.GameWinEvent.AddListener(ChangeColliderStatus);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;

		GameManager.Instance.PlayerFalled.RemoveListener(ChangeColliderStatus);

		GameManager.Instance.JumpedToFinish.RemoveListener(ChangeColliderStatus);
		GameManager.Instance.GameWinEvent.RemoveListener(ChangeColliderStatus);
	}
	private void CheckStickSize(Transform playerT)
	{
		var stick = playerT.GetComponentInChildren<PlayerStickController>();
		float playerPosX = playerT.position.x;

		if (stick.StickSize < distanceBetween * 2 - 0.5f)
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
		EventManager.EnteredRail.Invoke(this);

	}
	private void CheckFinishStick()
	{
		if (!isFinishStick)
			GameManager.Instance.PlayerFalled.Invoke();
		else
			GameManager.Instance.JumpedToFinish.Invoke();
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

			EventManager.ExitedRail.Invoke(this);
		}
	}

	private void ChangeColliderStatus()
	{
		foreach (var item in GetComponentsInChildren<Collider>())
			item.enabled = !item.enabled;
	}
}
