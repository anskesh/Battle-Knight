using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
	[SerializeField] private Button _restart;
	[SerializeField] private Button _next;

	public event UnityAction Restarted;
	public event UnityAction Next;

	private void OnEnable()
	{
		_restart.onClick.AddListener(OnRestart);
		_next.onClick.AddListener(OnNext);
	}

	private void OnDisable()
	{
		_restart.onClick.RemoveListener(OnRestart);
		_next.onClick.RemoveListener(OnNext);
	}

	private void OnRestart()
	{
		Restarted?.Invoke();
	}

	private void OnNext()
	{
		Next?.Invoke();
	}
}
