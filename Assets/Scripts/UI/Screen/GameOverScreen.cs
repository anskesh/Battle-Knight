using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] private Button _restart;
    
    public event UnityAction Restarted;
    
    private void OnEnable()
    {
        _restart.onClick.AddListener(OnRestart);
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestart);
    }

    private void OnRestart()
    {
        Restarted?.Invoke();
    }
}
