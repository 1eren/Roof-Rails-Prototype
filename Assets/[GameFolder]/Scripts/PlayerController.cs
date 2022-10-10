using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[ReadOnly] public bool isDeath;

	[ReadOnly] public bool isHolding;
	private void OnEnable()
	{
		GameManager.Instance.FallEvent.AddListener(Fall);
		EventManager.OnEnteredRail.AddListener(Hold);
		EventManager.OnExitRail.AddListener(Run);

	}
	private void OnDisable()
	{
		GameManager.Instance.FallEvent.RemoveListener(Fall);
		EventManager.OnEnteredRail.AddListener(Hold);
		EventManager.OnExitRail.RemoveListener(Run);
	}

	public void Hold(RailController rail)
	{
		isHolding = true;
		StartCoroutine(CheckHoldingStatus(rail, GetComponentInChildren<PlayerStickController>()));
	}
	public void Run(RailController rail)
	{
		GetComponent<CharacterAnimationController>().Run();
		isHolding = false;
	}
	public void Fall()
	{
		isDeath = true;
		FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
	}
	private IEnumerator CheckHoldingStatus(RailController rail, PlayerStickController stick)
	{
		while (isHolding && !isDeath)
		{
			Transform railT = rail.transform;
			float stickX = stick.transform.position.x;
			if (stickX + stick.StickSize / 2 < railT.position.x + rail.distanceBetween - 0.25f//0.25 is rail stick length
			|| stickX - stick.StickSize / 2 > railT.position.x - rail.distanceBetween + 0.25f)
			{
				if (rail.isFinishStick)
				{
					foreach (var item in rail.GetComponentsInChildren<BoxCollider>())
						item.enabled = false;
					EventManager.OnExitRail.Invoke(rail);
					break;
				}
				GameManager.Instance.FallEvent.Invoke();
				foreach (var item in rail.GetComponentsInChildren<BoxCollider>())
					item.enabled = false;
				isDeath = true;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
