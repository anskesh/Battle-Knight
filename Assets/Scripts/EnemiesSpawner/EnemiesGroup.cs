using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemiesGroup : MonoBehaviour
{
	[SerializeField] private float _range;
	[SerializeField] private List<int> _countEnemy;
	[SerializeField] private List<GameObject> _enemyTemplate;
	
	private List<Enemy> _enemies;
	private Player _player;
	
	public int CountEnemy { get; private set; }
	public event UnityAction EnemyCountChanged; 

	private void Awake()
	{
		CountEnemy = _countEnemy.Sum();
		_player = FindObjectOfType<Player>();
		_player.PlayerDied += OnPlayerDeath;
	}
	
	private void TakeTargetToEnemy()
	{
		_enemies.RemoveAll(item => item == null);
		foreach (var enemy in _enemies)
		{
			if (_player == null) enemy.StopFollowing();
			else
				enemy.GetTarget(_player);
		}
	}

	public void SpawnEnemies()
	{
		for (int i = 0; i < _countEnemy.Count; i++)
		{
			for (int j = 0; j < _countEnemy[i]; j++)
			{
				if (RandomPointPosition(transform.position, _range, out Vector3 result))
				{
					var enemy = Instantiate(_enemyTemplate[i], result, Quaternion.identity, transform);
					enemy.transform.LookAt(_player.transform);
					var enemyComponent = enemy.GetComponent<Enemy>();
					enemyComponent.EnemyDied += OnEnemyDied;
				}
			}
		}
	}

	private bool RandomPointPosition(Vector3 center, float radius, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			var randomPoint = center + Random.insideUnitSphere * radius;
			if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}

		result = Vector3.zero;
		return false;
	}

	public void GetAllEnemy()
	{
		_enemies = GetComponentsInChildren<Enemy>().ToList();
	}

	private void OnEnemyDied(Enemy enemy)
	{
		enemy.EnemyDied -= OnEnemyDied;
		GroupsController.CurrentCountEnemy--;
		CountEnemy--;
		EnemyCountChanged?.Invoke();
		if (CountEnemy == 0) Destroy(gameObject);
	}

	private void OnPlayerDeath()
	{
		_enemies.RemoveAll(item => item == null);
		foreach (var enemy in _enemies)
		{
			enemy.Celebration();
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_player = FindObjectOfType<Player>();
			TakeTargetToEnemy();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_player = null;
			TakeTargetToEnemy();
		}
	}
}
