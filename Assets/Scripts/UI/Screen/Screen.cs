using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;


    public void Open()
    {
        _group.alpha = 1;
        _group.interactable = true;
    }
}
