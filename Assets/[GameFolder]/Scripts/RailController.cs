using Sirenix.OdinInspector;
using UnityEngine;

public class RailController : MonoBehaviour
{
	[OnValueChanged("SetSticksAttributes")]
	[Range(0, 10)] public float distanceBetween = 1f;
	[Range(2, 30)] public float scale = 1f;

	[SerializeField] private float speedIncrease;
	public bool isFinishStick;
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
			coll.size = new Vector3(coll.size.x, coll.size.y, scale);
		}
	}

	private void CheckStickSize(Transform playerT)
	{
		var stick = playerT.GetComponentInChildren<PlayerStickController>();

		if (stick.StickSize < distanceBetween * 2)
		{
			if (!isFinishStick)
				GameManager.Instance.FallEvent.Invoke();
			foreach (var item in GetComponentsInChildren<BoxCollider>())
				item.enabled = false;
			return;
		}

		float playerPosX = playerT.position.x;
		if (playerPosX > transform.position.x + distanceBetween
			|| playerPosX < transform.position.x - distanceBetween)
		{
			if(!isFinishStick)
				GameManager.Instance.FallEvent.Invoke();
			foreach (var item in GetComponentsInChildren<BoxCollider>())
				item.enabled = false;
			return;
		}
		EventManager.OnEnteredRail.Invoke(this);

	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController player))
		{
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

}
