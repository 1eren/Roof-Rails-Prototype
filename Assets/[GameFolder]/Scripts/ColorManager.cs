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
public struct ColorData
{
	public GameColor color;
	public Material colorMat;
}
[DefaultExecutionOrder(-50)]
public class ColorManager : Singleton<ColorManager>
{
	public GameColor gameColor;

	public UnityEvent<GameColor> OnColorChange = new UnityEvent<GameColor>();

	public void ChangeColor(GameColor color)
	{
		gameColor = color;
		OnColorChange?.Invoke(gameColor);
	}
}
