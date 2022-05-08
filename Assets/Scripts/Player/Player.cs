using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, ICurable
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject _healing;

    private Animator _animator;
    private Collider _sword;
    private int _maxHealth;
    private int _money;
    
    public int Health => _health;
    public event UnityAction PlayerDied;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _money = 0;
        _animator = GetComponent<Animator>();
        _sword = GetComponentInChildren<Sword>().GetComponent<Collider>();
        _maxHealth = _health;
        HealthChanged?.Invoke(_health, _maxHealth);
        MoneyChanged?.Invoke(_money);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _animator.Play("GetHit_SingleTwohandSword");
        HealthChanged?.Invoke(_health, _maxHealth);
        if (_health <= 0) Died();
    }
    
    public bool AddHealth(int percent)
    {
        if (_health == _maxHealth) return false;
        Instantiate(_healing, transform);
        var health = _maxHealth * percent / 100;
        _health += health;
        if (_health > _maxHealth) _health = _maxHealth;
        HealthChanged?.Invoke(_health, _maxHealth);
        return true;
    }

    public void AddMoney(int money)
    {
        _money += money;
        MoneyChanged?.Invoke(_money);
    }

    public void EnableSwordCollider()
    {
        _sword.enabled = true;
    }
    public void DisableSwordCollider()
    {
        _sword.enabled = false;
    }
    
    private void Died()
    {
        _animator.StopPlayback();
        PlayerDied?.Invoke();
    }
}
