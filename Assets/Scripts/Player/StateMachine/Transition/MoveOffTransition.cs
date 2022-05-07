using System;
using UnityEngine;

public class MoveOffTransition : Transition
{
	[SerializeField] private Joystick _joystick;
	private void Update()
	{
		var x = _joystick.Horizontal;
		var z = _joystick.Vertical;
		
		if (Input.touchCount == 0)
			NeedTransit = true;
	}
}
