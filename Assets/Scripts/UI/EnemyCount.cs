using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _percent;
    [SerializeField] private GroupsController _groupsController;

    public event UnityAction GameEnded;
    
    private void OnEnable()
    {
        _groupsController.EnemyCountChanged += OnEnemyCountChanged;
    }

    private void OnDisable()
    {
        _groupsController.EnemyCountChanged -= OnEnemyCountChanged;
    }

    private void OnEnemyCountChanged(int current, int max)
    {
        var percent =  current * 100 /  max;
        _fill.fillAmount = current / (float) max;
        _percent.text = percent + "%";
        if (percent == 100) GameEnded?.Invoke();
    }
}
