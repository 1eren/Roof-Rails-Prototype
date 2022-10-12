using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	[ReadOnly] public bool isDeath;
	[ReadOnly] public bool isHolding;
	public GameColor color;

	[SerializeField] private GameObject deathParticle;
	private void OnEnable()
	{
		GameManager.Instance.PlayerFalled.AddListener(Fall);
		GameManager.Instance.PlayerDied.AddListener(Death);
		GameManager.Instance.JumpedToFinish.AddListener(() => isDeath = true);

		EventManager.EnteredRail.AddListener(Hold);
		EventManager.ExitedRail.AddListener(Run);

		ColorManager.Instance.OnColorChange.AddListener((x) => color = x);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.PlayerFalled.RemoveListener(Fall);
		GameManager.Instance.JumpedToFinish.RemoveListener(() => isDeath = true);
		GameManager.Instance.PlayerDied.RemoveListener(Death);

		EventManager.EnteredRail.RemoveListener(Hold);
		EventManager.ExitedRail.RemoveListener(Run);

		ColorManager.Instance.OnColorChange.RemoveListener((x) => color = x);
	}
	private void FixedUpdate()
	{
		CheckLava();
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
	public void Death()
	{
		FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
		isDeath = true;
		gameObject.SetActive(false);
		Instantiate(deathParticle, transform.position, Quaternion.identity);
	}
	private void CheckLava()
	{
		RaycastHit hit;
		Vector3 rayPoint = transform.position + Vector3.up * 3;
		if (Physics.Raycast(rayPoint, transform.TransformDirection(Vector3.down), out hit, 10f, TagLayerKeys.LAVA))
		{
			hit.transform.GetComponent<LavaController>().Burn();
		}
	}
	private IEnumerator CheckHoldingStatus(RailController rail, PlayerStickController stick)
	{
		while (isHolding && !isDeath)
		{
			Transform railT = rail.transform;
			float stickX = stick.transform.position.x;
			if (stickX + stick.StickSize / 2 < railT.position.x + rail.distanceBetween - rail.railWidth / 2
				|| stickX - stick.StickSize / 2 > railT.position.x - rail.distanceBetween + rail.railWidth / 2)
			{
				if (rail.isFinishStick)
				{
					isDeath = true;
					GameManager.Instance.JumpedToFinish.Invoke();
					EventManager.ExitedRail.Invoke(rail);
					break;
				}
				GameManager.Instance.PlayerFalled.Invoke();

				isDeath = true;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
