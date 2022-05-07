using UnityEngine;

public class EnemyCelebrationState : State
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
