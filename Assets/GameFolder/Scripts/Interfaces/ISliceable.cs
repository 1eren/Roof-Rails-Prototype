using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISliceable 
{
    public Material SlicedMaterial { get; }
    public void IncreaseScale(float amount, Vector3 direction);
    public void DecreaseScale(float amount, Vector3 direction);
}
