using UnityEngine;
using System.Linq;
using System;

public interface  IColorable
{
    ColorData[] ColorArray { get; }

    Renderer Mesh { get; }

    void ChangeColor(GameColor color);
}
