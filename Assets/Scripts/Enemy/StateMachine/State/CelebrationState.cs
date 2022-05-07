using UnityEngine;

public class CelebrationState : State
{
	private Animator _animator;
	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_animator.SetBool("IsCelebration", true);
	}
}
