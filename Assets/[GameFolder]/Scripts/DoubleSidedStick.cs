using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoubleSidedStick : MonoBehaviour
{
	[OnValueChanged("SetSticksAttributes")]
	[SerializeField] private float distanceBetween = 1f, scale = 1;

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
		}
	}

	private void CheckStickSize(Transform playerT)
	{
		var stick = playerT.GetComponentInChildren<PlayerStickController>();

		if (stick.StickSize < distanceBetween * 2)
			GameManager.Instance.FallEvent.Invoke();

		float playerPosX = playerT.position.x;
		if (playerPosX > transform.position.x + distanceBetween
			|| playerPosX < transform.position.x - distanceBetween)
		{
			GameManager.Instance.FallEvent.Invoke();
			foreach (var item in GetComponentsInChildren<BoxCollider>())
				item.enabled = false;
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController player))
		{
			if (player.isHolding && player.isDeath)
				return;
			player.Hold();

			CheckStickSize(player.transform);
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController player))
		{
			if (player.isDeath)
				return;
			var stick = player.GetComponentInChildren<PlayerStickController>();

			if (stick.transform.position.z + 0.2f >= transform.position.z + scale / 2f)
			{
				player.Run();
				return;
			}

			GameManager.Instance.FallEvent.Invoke();
			foreach (var item in GetComponentsInChildren<BoxCollider>())
				item.enabled = false;
		}
	}

}
