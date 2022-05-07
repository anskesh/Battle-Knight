using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private float _speed;

	private Rigidbody _rigidbody;
	private Animator _animator;
	
	public bool IsStop { get; set; }
	
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
	}

	private void OnDisable()
	{
		_animator.SetBool("IsWalk", false);
	}

	private void FixedUpdate()
	{
		if (IsStop)
		{
			_animator.SetBool("IsWalk", false);
			return;
		}
		var x = _joystick.Horizontal;
		var z = _joystick.Vertical;

		if (Math.Abs(x) > 0.05f || Math.Abs(z) > 0.05f)
		{
			var moveDirection = new Vector3(x, 0, z);
			_rigidbody.velocity = moveDirection * _speed;
			transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
			_animator.SetBool("IsWalk", true);
		}
		else
		{
			_rigidbody.velocity = Vector3.zero;
			_animator.SetBool("IsWalk", false);
		}
	}
}
