using UnityEngine;

public class Potion : MonoBehaviour, ICollectable
{
    [SerializeField] private int percentHeal;
    
    private float _speed = 1;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LootTakeZone"))
        {
            if (other.transform.parent.TryGetComponent(out ICurable curable))
                if (curable.AddHealth(percentHeal))
                {
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, _speed * Time.deltaTime);
                    Destroy(gameObject, 0.2f);
                }
        }
    }
}
