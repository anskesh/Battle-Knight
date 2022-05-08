using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] private int _health;
	[SerializeField] private List<GameObject> _collectables;
	[SerializeField] private GameObject _death;

	private Animator _animator;
	private int _maxHealth;

	public int Health => _health;
	public Player Target { get; private set; }
	public event UnityAction<Enemy> EnemyDied;
	public event UnityAction<int, int> HealthChanged;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_maxHealth = _health;
	}
	public void ApplyDamage(int damage)
	{
		_health -= damage;
		_animator.Play("GetHit");

		HealthChanged?.Invoke(_health, _maxHealth);
		
		if (_health <= 0) 
			Died();
	}
	
	public Transform GetTransform()
	{
		return transform;
	}

	public void GetTarget(Player target)
	{
		Target = target;
	}

	public void StopFollowing()
	{
		Target = null;
	}

	public void Celebration()
	{
		GetComponent<EnemyStateMachine>().Victory();
	}
	private void Died()
	{
		EnemyDied?.Invoke(this);
		foreach (var item in _collectables)
		{
			Instantiate(item, transform.position, Quaternion.identity, transform.parent);
		}

		Instantiate(_death, transform);
		Destroy(gameObject, 3f);
	}
}
