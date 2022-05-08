using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GroupsController : MonoBehaviour
{
    private int _allCountEnemy;
    private List<EnemiesGroup> _groups;

    public static int CurrentCountEnemy;
    public event UnityAction<int, int> EnemyCountChanged;
    public event UnityAction GameEnded;

    private void Start()
    {
        CurrentCountEnemy = 0;
        _groups = GetComponentsInChildren<EnemiesGroup>().ToList();
        SpawnEnemyAllGroups();
        EnemyCountChanged?.Invoke(_allCountEnemy - CurrentCountEnemy, _allCountEnemy);
    }

    private void OnDisable()
    {
        _groups.RemoveAll(item => item == null);
        foreach (var group in _groups)
        {
            group.EnemyCountChanged -= OnEnemyCountChanged;
        }
    }

    private void SpawnEnemyAllGroups()
    {
        foreach (var group in _groups)
        {
            group.EnemyCountChanged += OnEnemyCountChanged;
            group.SpawnEnemies();
            group.GetAllEnemy();
            CurrentCountEnemy += group.CountEnemy;
        }
        _allCountEnemy = CurrentCountEnemy;
    }

    private void OnEnemyCountChanged()
    {
        EnemyCountChanged?.Invoke(_allCountEnemy - CurrentCountEnemy, _allCountEnemy);
        if (CurrentCountEnemy == 0) GameEnded?.Invoke();
    }
}
