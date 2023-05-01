using System;
using UnityEngine;
using TMPro;

using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Color[] colors = { Color.white };
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private string highscorePrefix = "Highscore: ";
    
    public Action<int> OnScore;

    private Color _startColor;

    private bool _isDead;
    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = value.ToString();
        }
    }
    
    private void Start()
    {
        scoreText.text = "0";
        _startColor = scoreText.color;
    }

    private void OnEnable()
    {
        OnScore += PlayScoreEffect;
        GameStateHandler.OnGameOver += HandleGameOver;
    }

    private void HandleGameOver()
    {
        _isDead = true;
        HighscoreTracker.Instance.SubmitScore(_score);
        gameOverScore.text = _score.ToString();
        highscoreText.text = highscorePrefix + HighscoreTracker.Instance.GetHighscore();
    }

    private void OnDisable()
    {
        OnScore -= PlayScoreEffect;
        GameStateHandler.OnGameOver -= HandleGameOver;
    }

    private void PlayScoreEffect(int score)
    {
        scoreText.color = colors[Random.Range(0, colors.Length)];
        scoreText.transform.localScale = Vector3.one * 1.2f;
    }

    public void ScorePoints(int points)
    {
        if (_isDead) return;
        OnScore(points);
        Score += points;
    }

    private void Update()
    {
        scoreText.color = Color.Lerp(scoreText.color, _startColor, 6f * Time.deltaTime);
        scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, Vector3.one, 6f * Time.deltaTime);
    }
}
