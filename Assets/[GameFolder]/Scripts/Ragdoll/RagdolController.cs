using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class RagdolController : RagdollBase
{
	private void Awake()
	{
		DisableRagdoll();
	}
	private void OnEnable()
	{
		GameManager.Instance.FallEvent.AddListener(OnFailed);
	}
	private void OnDisable()
	{
		GameManager.Instance.FallEvent.RemoveListener(OnFailed);
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