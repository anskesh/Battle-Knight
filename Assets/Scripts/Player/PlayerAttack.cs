using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private Sword _sword;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private PlayerMover _playerMover;
    private bool _enemyNear;
    private List<IDamageable> _damageables = new List<IDamageable>();

    public int Damage { get; private set; }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerMover = FindObjectOfType<PlayerMover>();
        Damage = _damage;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (_damageables.Count > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
                StartCoroutine(AttackAnimation());
        }
    }

    private void OnDisable()
    {
        _animator.SetBool("IsAttack", false);
    }

    private IEnumerator AttackAnimation()
    {
        var timeForAnimation = 0.15f;
        while (_enemyNear && _damageables.Count > 0)
        {
            var enemy = _damageables[Random.Range(0, _damageables.Count)].GetObject().transform;
            transform.LookAt(enemy);
            _playerMover.IsStop = true;
            
            _animator.SetBool("IsAttack", true);
            _sword.GetComponent<Collider>().enabled = true;
            
            yield return new WaitForSeconds(0.01f);
            
            _animator.SetBool("IsAttack", false);
            
            yield return new WaitForSeconds(timeForAnimation);
            
            _sword.GetComponent<Collider>().enabled = false;
            _sword.ClearEnemy();
            _enemyNear = false;
            _damageables.Clear();
            
            yield return new WaitForSeconds(_timeBetweenAttack - timeForAnimation);
        }
        _sword.GetComponent<Collider>().enabled = false;
        _playerMover.IsStop = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            _enemyNear = true;
            if (!_damageables.Contains(damageable))
            {
                _damageables.Add(damageable);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemyNear = true;
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (!_damageables.Contains(damageable))
                _damageables.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (_damageables.Contains(damageable))
                _damageables.Remove(damageable);
        }
    }
}
