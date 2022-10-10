using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISliceable 
{
    public void IncreaseScale(float amount);
    public void Slice(Vector3 direction);
    public void CreateNewPart(float xScale, Vector3 hitPoint);
}
