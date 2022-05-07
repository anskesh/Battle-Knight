using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private float _timeBetweenAttack;
	
	private Animator _animator;
	private NavMeshAgent _agent;
	private Player _target;
	private float _timeAfterAttack;
	private bool _enemyNear;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(1f);
	}
	
	private void Update()
	{
		/*_timeAfterAttack += Time.deltaTime;
		if (_timeAfterAttack >= _timeBetweenAttack && _agent.remainingDistance <= _agent.stoppingDistance)
		{
			_animator.SetInteger("IDAction", 2);
		}*/
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Player player))
		{
			_target = player;
			_enemyNear = true;
		}
	}
}
