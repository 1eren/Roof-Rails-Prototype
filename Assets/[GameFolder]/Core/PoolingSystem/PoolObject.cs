﻿using UnityEngine;

public class PoolObject : MonoBehaviour
{
	private void OnDisable()
	{
		if (PoolingSystem.Instance == null)
			return;
		PoolingSystem ps = PoolingSystem.Instance;
		transform.position = ps.transform.position;
		transform.rotation = ps.transform.rotation;
	}
}
