using UnityEngine;

public class DestroyableObjects : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health == 0)
        {
            Died();
        }
    }
    
    public GameObject GetObject()
    {
        return gameObject;
    }
    
    private void Died()
    {
        Destroy(gameObject);
    }

}
