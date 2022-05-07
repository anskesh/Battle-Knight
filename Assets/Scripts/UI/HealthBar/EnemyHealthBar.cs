using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private Image _healthbar;
	[SerializeField] private Enemy _enemy;
	
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
	}

	private void OnEnable()
	{
		_enemy.HealthChanged += OnHealthChanged;
	}

	private void OnDisable()
	{
		_enemy.HealthChanged -= OnHealthChanged;
	}

	private void LateUpdate()
	{
		transform.LookAt(_camera.transform);
		transform.Rotate(0, 180, 0);
	}

	private void OnHealthChanged(int currentHealth, int maxHealth)
	{
		if (currentHealth != maxHealth) 
			_healthbar.gameObject.SetActive(true);
		else 
			_healthbar.gameObject.SetActive(false);
        
		_image.fillAmount = (float) currentHealth / maxHealth;
	}
}
