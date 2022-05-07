using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveState : State
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private float _speed;

	private Rigidbody _rigidbody;
	private Animator _animator;
	private Quaternion _rotation;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
	}

	private void OnDisable()
	{
		_rigidbody.velocity = Vector3.zero;
		_animator.SetBool("IsWalk", false);
	}

	private void FixedUpdate()
	{
		var x = _joystick.Horizontal;
		var z = _joystick.Vertical;
		
		var moveDirection = new Vector3(x, 0, z);
		_rigidbody.velocity = moveDirection * _speed;
		if (_rigidbody.velocity != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
		_animator.SetBool("IsWalk", true);
	}
}
