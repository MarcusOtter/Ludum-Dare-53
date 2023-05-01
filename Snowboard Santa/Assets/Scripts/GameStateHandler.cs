using UnityEngine;
using UnityEngine.Events;

public class GameStateHandler : MonoBehaviour
{
	[SerializeField] private UnityEvent OnGameStart;
	[SerializeField] private UnityEvent OnGameOver;

	private void OnEnable()
	{
		GameOverCollider.OnPlayerDeath += GameOver;
	}

	private void StartGame()
	{
		OnGameStart?.Invoke();
	}

	private void GameOver()
	{
		OnGameOver?.Invoke();
	}

	private void OnDisable()
	{
		GameOverCollider.OnPlayerDeath -= GameOver;
	}
}
