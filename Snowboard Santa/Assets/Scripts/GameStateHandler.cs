using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameStateHandler : MonoBehaviour
{
	public static Action OnGameOver;
	
	[FormerlySerializedAs("OnGameOver")]
	[SerializeField] private UnityEvent UnityOnGameOver;
	public static bool GameEnded = false;

	private void OnEnable()
	{
		GameEnded = false;
		GameOverCollider.OnPlayerDeath += GameOver;
	}

	private void GameOver()
	{
		if(GameEnded) { return; }

		UnityOnGameOver?.Invoke();
		OnGameOver?.Invoke();
		GameEnded = true;
	}

	private void OnDisable()
	{
		GameOverCollider.OnPlayerDeath -= GameOver;
	}
}
