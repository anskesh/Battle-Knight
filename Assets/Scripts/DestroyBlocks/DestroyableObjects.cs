using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private List<GameObject> _collectables;

    public int Health => _health;
    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health == 0)
        {
            Died();
        }
    }
    
    public Transform GetTransform()
    {
        return transform;
    }
    
    private void Died()
    {
        Instantiate(_collectables[0], transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

}
