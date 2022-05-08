using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _healthbar;
    
    private Player _player;
    private Camera _camera;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform);
        transform.Rotate(0, 180, 0);
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        _image.fillAmount = (float) currentHealth / maxHealth;
        
        if (currentHealth < maxHealth) 
            _healthbar.gameObject.SetActive(true);
        else 
            _healthbar.gameObject.SetActive(false);
        
    }
}
