using System;
using UnityEngine;

public class MoveTransition : Transition
{
	[SerializeField] private Joystick _joystick;
	private void Update()
	{
		var x = _joystick.Horizontal;
		var z = _joystick.Vertical;

		if (Math.Abs(x) > 0.05f || Math.Abs(z) > 0.05f)
			NeedTransit = true;
	}
}
