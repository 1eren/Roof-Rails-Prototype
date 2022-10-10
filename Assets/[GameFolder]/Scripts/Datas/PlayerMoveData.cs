using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Sciptable Objects/Move Data")]
public class PlayerMoveData : ScriptableObject
{
    public float horizontalSpeed = 1f, forwardSpeed = 5f;

    public float xClampMin, xClampMax;
}
