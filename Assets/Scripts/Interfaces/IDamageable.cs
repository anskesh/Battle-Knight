using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int Health { get; }
    public void ApplyDamage(int damage);

    public Transform GetTransform();
}
