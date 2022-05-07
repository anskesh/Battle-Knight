using UnityEngine;

public class Potion : MonoBehaviour, ICollectable
{
    [SerializeField] private int percentHeal;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out ICurable curable))
            if (curable.AddHealth(percentHeal))
                Destroy(gameObject);
    }
}
