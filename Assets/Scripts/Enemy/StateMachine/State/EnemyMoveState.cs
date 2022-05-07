using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : State
{
	private NavMeshAgent _agent;
	private Animator _animator;
	
	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_agent.isStopped = false;
	}

	private void OnDisable()
	{
		_agent.isStopped = true;
		_animator.SetBool("IsWalk", false);
	}

	private void Update()
	{
		_agent.SetDestination(Target.transform.position);
		if (_agent.remainingDistance <= _agent.stoppingDistance)
			_animator.SetBool("IsWalk", false);
		else
			_animator.SetBool("IsWalk", true);
	}
}
