using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Random = UnityEngine.Random;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Color[] colors = { Color.red, Color.green };
    [SerializeField] private TextMeshProUGUI scoreText;
    public Action<int> OnScore;

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = value.ToString();
        }
    }
    private static ScoreManager instance;


    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        instance = this;
        OnScore += PlayScoreEffect;
    }
    private void OnDisable()
    {
        OnScore -= PlayScoreEffect;
    }

    private void PlayScoreEffect(int score)
    {
        scoreText.color = colors[Random.Range(0, colors.Length)];
        scoreText.transform.localScale = Vector3.one * 1.2f;
    }

    public static void ScorePoints(int points)
    {
        if(instance == null)
        {
            // Debug.LogWarning("There is no score manager!");
            return;
        }
        instance.OnScore(points);
        instance.Score += points;
    }

    private void Update()
    {
        scoreText.color = Color.Lerp(scoreText.color, Color.white, 6f * Time.deltaTime);
        scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, Vector3.one, 6f * Time.deltaTime);
    }
}
