using UnityEngine;

public class AttackOffTransition : Transition
{
    private float _radius;
    private bool _enemyNear;
    private void Update()
    {
        if (_enemyNear == false)
            NeedTransit = true;
        _enemyNear = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable)) _enemyNear = true;
    }
}
