using System;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    private Animator _animator;
    private float _elapsedTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_elapsedTime >= _cooldown)
        {
            Attack();
            _elapsedTime = 0;
        }

        _elapsedTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        _animator.SetBool("IsAttack", false);
    }

    private void Attack()
    {
        transform.LookAt(Target.transform);
        _animator.SetBool("IsAttack", true);
        Target.ApplyDamage(_damage);
    }
}
