using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour, ICollectable
{
	public virtual void Collect(GemCollector collector)
	{
		Destroy(gameObject);
		PoolingSystem.Instance.InstantiateAPS("CollectableParticle", transform.position);
		EventManager.OnGemCollected.Invoke(transform.position);
	}
}
