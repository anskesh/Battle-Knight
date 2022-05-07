using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerAttackState : State
{
	[SerializeField] private int _damage;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private Sword _sword;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private IDamageable _target;
    private IEnumerator _attackEnemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _sword.Damage = _damage;
    }

    private void OnEnable()
    {
        _attackEnemy = AttackAnimation();
        StartCoroutine(_attackEnemy);
    }

    private void OnDisable()
    {
        StopCoroutine(_attackEnemy);
        _target = null;
        _sword.GetComponent<Collider>().enabled = false;
        _animator.SetBool("IsAttack", false);
        _agent.isStopped = true;
    }

    private IEnumerator AttackAnimation()
    {
        var timeForAnimation = 0.20f;
        while (_target != null)
        {
            var targetTransform = _target.GetTransform();
            transform.LookAt(targetTransform);
            _agent.SetDestination(targetTransform.position);
            _agent.isStopped = false;
            
            _sword.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(0.01f);
            _animator.Play("NormalAttack01_SingleTwohandSword");
            yield return new WaitForSeconds(0.01f);
            _animator.StopPlayback();

            yield return new WaitForSeconds(timeForAnimation);

            _agent.isStopped = true;
            _sword.GetComponent<Collider>().enabled = false;
            _sword.ClearEnemy();
            _target = null;
            
            yield return new WaitForSeconds(_timeBetweenAttack - timeForAnimation);
        }
        _sword.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (_target == null) 
                _target = damageable;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (_target == null) 
                _target = damageable;
        }
    }
}
