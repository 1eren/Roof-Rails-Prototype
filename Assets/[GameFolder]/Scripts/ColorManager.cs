using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum GameColor
{
	Blue,
	Green,
	Red
}
[System.Serializable]
public struct ColorList
{
	public GameColor color;
	public Material colorMat;
}
public class ColorManager : Singleton<ColorManager>
{
	public List<ColorList> colorList = new List<ColorList>();
	GameColor color;

	public UnityEvent<GameColor> OnColorChange = new UnityEvent<GameColor>();
	private void OnEnable()
	{
		OnColorChange.AddListener((x) => color = x);
	}
	public void ChangeColor(MeshRenderer mesh)
	{
		foreach (var item in colorList)
		{
			if (item.color == color)
				mesh.material = item.colorMat;
		}
	}
}
