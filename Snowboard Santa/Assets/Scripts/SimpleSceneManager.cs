using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
	public static void ReloadScene()
	{
		SceneManager.LoadScene(CurrentSceneIndex);
	}

	public static bool HasNextScene()
	{
		return CurrentSceneIndex + 1 <= HighestSceneIndex;
	}

	public static bool HasPreviousScene()
	{
		return CurrentSceneIndex > 0;
	}
	
	public static void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}
	
	public static void LoadNextScene()
	{
		if (HasNextScene())
		{
			SceneManager.LoadScene(CurrentSceneIndex + 1);
		}
	}

	public static void LoadPreviousScene()
	{
		if (HasPreviousScene())
		{
			SceneManager.LoadScene(CurrentSceneIndex - 1);
		}
	}

	private static int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
	private static int HighestSceneIndex => SceneManager.sceneCountInBuildSettings - 1;
}
