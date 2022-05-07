using System;
using UnityEngine;

public class DistanceOffTransition : Transition
{
	[SerializeField] private float _radius;
	
	private Player _target;

	private void Start()
	{
		_target = FindObjectOfType<Player>();
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, FindObjectOfType<Player>().transform.position) >= _radius)
			NeedTransit = true;
	}
}
