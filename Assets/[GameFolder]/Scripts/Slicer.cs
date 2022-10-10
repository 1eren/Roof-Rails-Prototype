using UnityEngine;

public class Slicer : MonoBehaviour
{
	private bool isTriggered;
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out ISliceable sliceable))
		{
			GetComponent<Collider>().enabled = false;
			isTriggered = true;
			sliceable.Slice(transform.position);
		}
	}
}
