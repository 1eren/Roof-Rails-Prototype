using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{

	private void Update()
	{
		OnTapHoldInput();
	}

	#region TapHoldInput

	public UnityEvent OnTapDown = new UnityEvent();
	public UnityEvent OnTapUp = new UnityEvent();
	[ReadOnly] public bool isDown;

	public void OnTapHoldInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnTapDown.Invoke();
			isDown = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			OnTapUp.Invoke();
			isDown = false;
		}
	}
	#endregion
}
