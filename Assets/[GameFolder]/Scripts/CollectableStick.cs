using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStick : MonoBehaviour, IColorable
{
	[OnValueChanged("ChangeColor")] public GameColor color;

	[SerializeField] ColorData[] colorArray;
	public ColorData[] ColorArray => colorArray;

	private Renderer mesh;
	public Renderer Mesh => mesh == null ? mesh = GetComponent<MeshRenderer>() : mesh;

	public float increasingScale = 0.1f;
	private bool isTriggered;


	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.TryGetComponent(out PlayerController player))
		{
			isTriggered = true;
			if (player.color != color)
				return;
			gameObject.SetActive(false);
			player.GetComponentInChildren<ISliceable>().IncreaseScale(increasingScale);
		}
	}
	public void ChangeColor(GameColor color)
	{
		foreach (var item in ColorArray)
		{
			if (item.color == this.color)
				Mesh.material = item.colorMat;
		}
	}
}
