using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlicedStickController : MonoBehaviour, IThrowable
{
	[SerializeField] private float destroyingDelay = 3;
	public void Throw(float force, Vector3 direction)
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(direction * force, ForceMode.Impulse);

		if (TryGetComponent(out PoolObject pool))
			Run.After(destroyingDelay, () => PoolingSystem.Instance.DestroyAPS(gameObject));
	}
}
