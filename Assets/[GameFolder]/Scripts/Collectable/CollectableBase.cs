using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour, ICollectable
{
	[SerializeField] private bool uiCollectEffect;
	public virtual void Collect(Collector collector)
	{
		Destroy(gameObject);
		PoolingSystem.Instance.InstantiateAPS("CollectableParticle", transform.position);
		EventManager.OnGemCollected.Invoke(transform.position);
		if (uiCollectEffect)
		{
			PlayerPrefs.SetInt(PlayerPrefKeys.COIN, PlayerPrefs.GetInt(PlayerPrefKeys.COIN) + 1);
			//EventManager.OnGemCollected.Invoke(transform.position, () => { });
		}
	}
}
