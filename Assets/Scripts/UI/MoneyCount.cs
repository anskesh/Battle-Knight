using System;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Player _player;

	private void OnEnable()
	{
		_player.MoneyChanged += OnMoneyChanged;
	}

	private void OnDisable()
	{
		_player.MoneyChanged -= OnMoneyChanged;
	}

	private void OnMoneyChanged(int money)
	{
		_text.text = money.ToString();
	}
}
