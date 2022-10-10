using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
	private void OnDisable()
	{
		PoolingSystem ps = PoolingSystem.Instance;
		transform.position = ps.transform.position;
		transform.rotation = ps.transform.rotation;
		transform.SetParent(ps.transform);
	}
}
