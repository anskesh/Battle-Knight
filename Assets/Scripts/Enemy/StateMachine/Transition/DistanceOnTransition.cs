using System;
using UnityEngine;

public class DistanceOnTransition : Transition
{
	[SerializeField] private float _radius;
	
	private Player _target;

	private void Start()
	{
		_target = FindObjectOfType<Player>();
	}
	private void Update()
	{
		if (Vector3.Distance(transform.position, _target.transform.position) <= _radius)
			NeedTransit = true;
	}
}
