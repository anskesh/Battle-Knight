using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private float _detectorDisablingDistance;
    [SerializeField] private Transform _playerTransform;
    
    private List<EnemiesGroup> _enemiesGroups = new List<EnemiesGroup>();
    private Transform _target;
    private float _currentDistance;

    private void Start()
    {
        _enemiesGroups = FindObjectsOfType<EnemiesGroup>().ToList();
        _target = _enemiesGroups[0].transform;
        _currentDistance = Vector3.Distance(_playerTransform.position, _target.position);
        FindClosestGroup();
    }

    private void Update()
    {
        _enemiesGroups.RemoveAll(item => item == null);
        if (_enemiesGroups.Count == 0) return;
        if (_target == null)
        {
            _target = _enemiesGroups[0].transform;
            _currentDistance = Vector3.Distance(_playerTransform.position, _target.transform.position);
        }
        FindClosestGroup();
        if (Vector3.Distance(_playerTransform.position, _target.position) <= _detectorDisablingDistance) _image.gameObject.SetActive(false);
        else
        {
            transform.LookAt(_target);
            _image.gameObject.SetActive(true);
        }
        
    }

    private void FindClosestGroup()
    {
        _enemiesGroups.RemoveAll(item => item == null);
        _currentDistance = Vector3.Distance(_playerTransform.position, _target.transform.position);
        foreach (var group in _enemiesGroups)
        {
            var distance = Vector3.Distance(_playerTransform.position, group.transform.position);
            if (distance < _currentDistance)
            {
                _currentDistance = distance; 
                _target = group.transform;
            }
        }
        
    }
}
