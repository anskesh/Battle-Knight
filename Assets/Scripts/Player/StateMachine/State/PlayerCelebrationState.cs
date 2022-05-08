using UnityEngine;

public class PlayerCelebrationState : State
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		transform.LookAt(Camera.main.transform);
		_animator.Play("Victory_SingleTwohandSword");
	}
}
