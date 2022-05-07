using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour, ICollectable
{
	private int _countMoney;

	private void Awake()
	{
		_countMoney = Random.Range(10, 30);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.TryGetComponent(out Player player))
		{
			player.AddMoney(_countMoney);
			Destroy(gameObject);
		}
		
	}
}
