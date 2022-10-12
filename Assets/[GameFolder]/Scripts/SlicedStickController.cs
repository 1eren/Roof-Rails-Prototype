using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlicedStickController : MonoBehaviour, IThrowable
{
	[SerializeField] private float destroyingDelay = 3;

	private Rigidbody rigidbody;
	public Rigidbody Rigidbody => rigidbody == null ? rigidbody = GetComponent<Rigidbody>() : rigidbody;

	private void OnEnable()
	{
		ColorManager cm = ColorManager.Instance;
		cm.ChangeMaterial(GetComponent<MeshRenderer>(), cm.gameColor);
	}
	private void OnDisable()
	{
		Rigidbody.velocity = Vector3.zero;
		Rigidbody.angularVelocity = Vector3.zero;
	}
	public void Throw(Vector3 force)
	{
		Rigidbody.AddForce(force, ForceMode.Impulse);

		if (TryGetComponent(out PoolObject pool))
			Run.After(destroyingDelay, () => PoolingSystem.Instance.DestroyAPS(gameObject));
	}
}
