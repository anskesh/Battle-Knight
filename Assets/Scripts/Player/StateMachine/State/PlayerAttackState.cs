using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttackState : State
{
	[SerializeField] private int _damage;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private Sword _sword;
    
    private Animator _animator;
    private PlayerMover _playerMover;
    private bool _enemyNear;
    private Transform _target;
    private List<IDamageable> _damageables = new List<IDamageable>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sword.Damage = _damage;
    }

    private void OnEnable()
    {
        StartCoroutine(AttackAnimation());
    }

    private void OnDisable()
    {
        _animator.SetBool("IsAttack", false);
    }

    private IEnumerator AttackAnimation()
    {
        var timeForAnimation = 0.15f;
        while (_target != null)
        {
            transform.LookAt(_target);
            
            _sword.GetComponent<Collider>().enabled = true;
            _animator.SetBool("IsAttack", true);
            
            yield return new WaitForSeconds(0.01f);
            
            _animator.SetBool("IsAttack", false);
            
            yield return new WaitForSeconds(timeForAnimation);
            
            _sword.GetComponent<Collider>().enabled = false;
            _sword.ClearEnemy();
            /*_damageables.Clear();*/
            
            yield return new WaitForSeconds(_timeBetweenAttack - timeForAnimation);
        }
        _sword.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            _enemyNear = true;
            if (_target != null) return;
            _target = damageable.GetObject().transform;
            /*if (!_damageables.Contains(damageable))
            {
                _damageables.Add(damageable);
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemyNear = true;
        /*if (other.TryGetComponent(out IDamageable damageable))
        {
            if (!_damageables.Contains(damageable))
                _damageables.Add(damageable);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.TryGetComponent(out IDamageable damageable))
        {
            if (_damageables.Contains(damageable))
                _damageables.Remove(damageable);
        }*/
    }
}
