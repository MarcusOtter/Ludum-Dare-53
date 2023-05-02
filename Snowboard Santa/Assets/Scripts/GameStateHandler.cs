using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameStateHandler : MonoBehaviour
{
	public static Action OnGameOver;
	public static float TimeAlive { get; private set; }
	
	[FormerlySerializedAs("OnGameOver")]
	[SerializeField] private UnityEvent UnityOnGameOver;
	
	public static bool GameEnded = false;

	private void OnEnable()
	{
		GameEnded = false;
		GameOverCollider.OnPlayerDeath += GameOver;
		TimeAlive = 0f;
	}

	private void Update()
	{
		if (GameEnded) return;
		TimeAlive += Time.deltaTime;
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
