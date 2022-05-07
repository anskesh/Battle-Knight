using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PlayerHealthBar _healthBar;

    private Animator _animator;
    private int _maxHealth;
    
    public event UnityAction PlayerDied;
    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _maxHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _animator.SetBool("IsHit", true);
        _animator.SetBool("IsHit", false);
        HealthChanged?.Invoke(_health, _maxHealth);
        if (_health <= 0) Died();
    }

    private void Died()
    {
        _healthBar.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;/*
        GetComponent<PlayerMover>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;*/
        FindObjectOfType<Sword>().enabled = false;
        _animator.SetBool("IsDeath", true);
        PlayerDied?.Invoke();
    }
}
