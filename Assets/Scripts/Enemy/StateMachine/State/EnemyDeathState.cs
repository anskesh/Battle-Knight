using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathState : State
{
	[SerializeField] private EnemyHealthBar _healthBar;
	
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		_animator.StopPlayback();
		_animator.SetBool("IsDeath", true);
		_healthBar.gameObject.SetActive(false);
		GetComponent<Collider>().enabled = false;
		enabled = false;
	}
}
