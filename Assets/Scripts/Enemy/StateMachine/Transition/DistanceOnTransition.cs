using System;
using UnityEngine;

public class DistanceOnTransition : Transition
{
	[SerializeField] private float _radius;
	private void Update()
	{
		if (Vector3.Distance(transform.position, FindObjectOfType<Player>().transform.position) <= _radius)
			NeedTransit = true;
	}
}
