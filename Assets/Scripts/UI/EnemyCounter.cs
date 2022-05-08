using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _percent;
    
    private GroupsController _groupsController;

    private void OnEnable()
    {
        _groupsController = FindObjectOfType<GroupsController>();
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
    }
}
