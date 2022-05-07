using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private List<IDamageable> _enemiesDamaged = new List<IDamageable>();

    public int Damage { get; set; }

    public void ClearEnemy()
    {
        if (_enemiesDamaged.Count > 0)
            _enemiesDamaged.Clear();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (!_enemiesDamaged.Contains(damageable))
            {
                damageable.ApplyDamage(Damage);
                _enemiesDamaged.Add(damageable);
            }
        }
    }
}
