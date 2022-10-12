using UnityEngine;

public class LavaController : MonoBehaviour
{
	public float amount;
	[SerializeField] private float destroyingDestiny = 0.2f;

	private float time = 0;
	public void Burn()
	{
		time += Time.deltaTime;
		if (time > destroyingDestiny)
		{
			time = 0;
			EventManager.StickDecrased.Invoke(amount);
		}
	}
}
