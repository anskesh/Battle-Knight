using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour, ICollectable
{
	private int _countMoney;
	private float _speed = 1;
	
	private void Awake()
	{
		_countMoney = Random.Range(10, 30);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LootTakeZone"))
		{
			if (other.transform.parent.TryGetComponent(out Player player))
			{
				player.AddMoney(_countMoney);
				transform.position = Vector3.MoveTowards(transform.position, other.transform.position, _speed * Time.deltaTime);
				Destroy(gameObject, 0.2f);
			}
		}
	}
}
