using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameStateHandler : MonoBehaviour
{
	public static Action OnGameOver;
	
	[FormerlySerializedAs("OnGameOver")]
	[SerializeField] private UnityEvent UnityOnGameOver;

	private void OnEnable()
	{
		GameOverCollider.OnPlayerDeath += GameOver;
	}

	private void GameOver()
	{
		UnityOnGameOver?.Invoke();
		OnGameOver?.Invoke();
	}

	private void OnDisable()
	{
		GameOverCollider.OnPlayerDeath -= GameOver;
	}
}
