using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TerrainUtils;

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
	public GameColor gameColor;

	public UnityEvent<GameColor> OnColorChange = new UnityEvent<GameColor>();

	public void ChangeColor(GameColor color)
	{
		gameColor = color;
		OnColorChange?.Invoke(gameColor);
	}
	public void ChangeMaterial(MeshRenderer mesh, GameColor? color)
	{
		foreach (var item in colorList)
		{
			if (color != null)
			{
				if (item.color == color)
					mesh.material = item.colorMat;
			}
			else
			{
				if (item.color == gameColor)
					mesh.material = item.colorMat;
			}
		}
	}
}
