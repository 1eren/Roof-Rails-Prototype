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
		if (LevelManager.Instance == null)
			return;
		EventManager.OnFailEvent.AddListener(OnFailed);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null)
			return;
		EventManager.OnFailEvent.AddListener(OnFailed);
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
		RagdollWithForce(transform.forward, 8f);
	}
}