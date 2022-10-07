using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlicedStickController : MonoBehaviour, IThrowable
{
	[SerializeField] private float destroyingDelay = 3;
	private Rigidbody rb;
	private void OnDisable()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	public void Throw(float force, Vector3 direction)
	{
		rb = GetComponent<Rigidbody>();

		rb.AddForce(direction * force, ForceMode.Impulse);

		if (TryGetComponent(out PoolObject pool))
			Run.After(destroyingDelay, () => PoolingSystem.Instance.DestroyAPS(gameObject));
	}
}
