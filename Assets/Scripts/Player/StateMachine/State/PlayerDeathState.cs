using UnityEngine;

public class PlayerDeathState : State
{
	[SerializeField] private PlayerHealthBar _healthBar;
	
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_animator.StopPlayback();
		_animator.SetBool("IsDeath", true);
		_healthBar.gameObject.SetActive(false);
		GetComponent<Collider>().enabled = false;
	}
}
