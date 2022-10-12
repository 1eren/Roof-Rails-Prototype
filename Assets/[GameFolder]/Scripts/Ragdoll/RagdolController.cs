using UnityEngine;
public class RagdolController : RagdollBase
{
	private void Awake()
	{
		DisableRagdoll();
	}
	private void OnEnable()
	{
		GameManager.Instance.PlayerFalled.AddListener(OnFailed);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.PlayerFalled.RemoveListener(OnFailed);
	}

	public void FreeFall()
	{
		ActivateRagdoll();
	}
	public void RagdollWithForce(Vector3 direction, float force)
	{
		ActivateRagdoll();
		AddForceToRagdollObject(direction, force);
	}

	public void OnFailed()
	{
		Run.After(0.5f, ()=>RagdollWithForce(transform.forward, 3f));
	}
}