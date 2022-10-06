using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Datas", menuName = "Datas/CharacterData", order = 100)]
public class MovementData : ScriptableObject
{
	public float speed = 5f;
	public float swerveSpeed = 1f;
	public float clampMovement = 3f;
}
