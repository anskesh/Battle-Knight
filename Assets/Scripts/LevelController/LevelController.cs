using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	[SerializeField] private EnemyCounter _enemyCounter;
	[SerializeField] private VictoryScreen _victoryScreen;
	[SerializeField] private GameOverScreen _gameOverScreen;
	[SerializeField] private List<GameObject> _levels;
	[SerializeField] private Player _player;

	private GroupsController _groups;
	private SpawnPoint _spawn;
	private Joystick _joystick;
	private int _currentLevel;
	
	private void Awake()
	{
		if (!PlayerPrefs.HasKey("currentLevel") || PlayerPrefs.GetInt("currentLevel") == _levels.Count)
			PlayerPrefs.SetInt("currentLevel", 0);
		_currentLevel = PlayerPrefs.GetInt("currentLevel");
		LoadLevel(_currentLevel);
		_joystick = FindObjectOfType<Joystick>();
	}

	private void LoadLevel(int numberLevel)
	{
		Instantiate(_levels[numberLevel]);
		_groups = FindObjectOfType<GroupsController>();
		_groups.GameEnded += OnGameEnded;
		_enemyCounter.enabled = true;
		_spawn = FindObjectOfType<SpawnPoint>();
		_player.transform.position = _spawn.transform.position;
	}
	
	private void OnEnable()
	{
		_player.PlayerDied += OnPlayerDied;
		_victoryScreen.Next += OnNextLevelButton;
		_victoryScreen.Restarted += OnRestart;
		_gameOverScreen.Restarted += OnRestart;
	}

	private void OnDisable()
	{
		_groups.GameEnded -= OnGameEnded;
		_victoryScreen.Next -= OnNextLevelButton;
		_victoryScreen.Restarted -= OnRestart;
		_gameOverScreen.Restarted -= OnRestart;
	}

	private void EnableVictoryScreen()
	{
		_victoryScreen.Open();
	}

	private void EnableGameOverScreen()
	{
		_gameOverScreen.Open();
	}
	
	private void OnGameEnded()
	{
		_enemyCounter.gameObject.SetActive(false);
		_player.GetComponent<PlayerStateMachine>().Victory();
		EnableVictoryScreen();
	}

	private void OnPlayerDied()
	{
		_player.PlayerDied -= OnPlayerDied;
		_enemyCounter.gameObject.SetActive(false);
		_joystick.gameObject.SetActive(false);
		EnableGameOverScreen();
	}

	private void OnNextLevelButton()
	{
		_currentLevel++;
		PlayerPrefs.SetInt("currentLevel", _currentLevel);
		SceneManager.LoadScene(0);
	}

	private void OnRestart()
	{
		SceneManager.LoadScene(0);
	}
}
