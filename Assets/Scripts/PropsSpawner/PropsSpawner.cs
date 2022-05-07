using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropsSpawner : MonoBehaviour
{
	[SerializeField] private List<GameObject> _templates;
	
	private List<Transform> _spawnpoints = new List<Transform>();

	private void Start()
	{
		_spawnpoints = GetComponentsInChildren<Transform>().ToList();
		_spawnpoints.RemoveAt(0);
		Spawn();
	}

	private void Spawn()
	{
		foreach (var spawnpoint in _spawnpoints)
		{
			var prop = Instantiate(_templates[Random.Range(0, _templates.Count)], spawnpoint);
			prop.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		}
	}
}
