using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISliceable 
{
    public Material SlicedMaterial { get; }
    public void IncreaseScale(float amount);
    public void DecreaseScale(Vector3 direction);
}
