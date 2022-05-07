using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] private int _health;
	[SerializeField] private List<GameObject> _collectables;

	private Animator _animator;
	private bool _targetInZone;
	private int _maxHealth;

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
		StartCoroutine(HitAnimation(damage));

		HealthChanged?.Invoke(_health, _maxHealth);
		
		if (_health <= 0) 
			Died();
	}

	private IEnumerator HitAnimation(int damage)
	{
		_animator.SetBool("IsGetHit", true);
		yield return new WaitForSeconds(0.01f);
		_health -= damage;
		_animator.SetBool("IsGetHit", false);
	}
	public GameObject GetObject()
	{
		return gameObject;
	}

	public void GetTarget(Player target)
	{
		Target = target;
		_targetInZone = true;
	}

	public void StopFollowing()
	{
		Target = null;
		_targetInZone = false;
	}

	public void Celebration()
	{
		GetComponent<EnemyStateMachine>().OnPlayerDied();
		
	}
	private void Died()
	{
		/*Instantiate(_collectables[0], transform);*/
		EnemyDied?.Invoke(this);
		Destroy(gameObject);
	}
}
