using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private List<GameObject> _collectables;

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
        foreach (var item in _collectables)
        {
            Instantiate(item, transform.position, Quaternion.identity, transform.parent);
        }
        Destroy(gameObject);
    }

}
