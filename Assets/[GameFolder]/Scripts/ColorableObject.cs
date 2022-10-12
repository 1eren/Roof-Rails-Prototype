using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableObject : MonoBehaviour, IColorable
{

	[SerializeField] ColorData[] colorArray;

	public ColorData[] ColorArray => colorArray;

	private Renderer mesh;
	public Renderer Mesh => mesh == null ? mesh = GetComponent<MeshRenderer>() : mesh;
	private void OnEnable()
	{
		if (ColorManager.Instance == null) return;
		ColorManager.Instance.OnColorChange.AddListener(ChangeColor);
	}
	private void OnDisable()
	{
		if (ColorManager.Instance == null) return;
		ColorManager.Instance.OnColorChange.AddListener(ChangeColor);
	}
	public void ChangeColor(GameColor color)
	{
		foreach (var item in ColorArray)
		{
			if (item.color == color)
				Mesh.material = item.colorMat;
		}
	}
}
