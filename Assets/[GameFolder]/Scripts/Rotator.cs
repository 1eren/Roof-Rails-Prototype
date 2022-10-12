using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Vector3 dir;
	void Update()
	{
		transform.Rotate(dir*Time.deltaTime, Space.Self);
	}
}
