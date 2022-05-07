using UnityEngine;

public class AttackTransition : Transition
{
	private float _radius;
	private bool _enemyNear;
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Ended && _enemyNear)
				NeedTransit = true;
		}
		_enemyNear = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent(out IDamageable damageable)) _enemyNear = true;
	}
}
