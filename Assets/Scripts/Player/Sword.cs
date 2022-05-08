using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Damage);
        }
    }
}
