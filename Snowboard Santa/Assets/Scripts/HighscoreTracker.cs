using UnityEngine;

public class HighscoreTracker : MonoBehaviour
{
	public static HighscoreTracker Instance { get; private set; }

	private int _highscore;
	
	private void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void SubmitScore(int score)
	{
		if (score > _highscore)
		{
			_highscore = score;
		}
	}
	
	public int GetHighscore()
	{
		return _highscore;
	}
}
