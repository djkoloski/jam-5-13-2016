using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FadeToBlack : MonoBehaviour
{
	// Static variables
	public static FadeToBlack instance;

	// Public variables
	public string nextScene;

	// Private variables
	private Animator animator_;

	// Initialization
	public void Awake()
	{
		instance = this;

		animator_ = GetComponent<Animator>();
	}

	// Public interface
	public void FadeOut()
	{
		animator_.Play("fadeToBlack");
	}
	public void OnFadeToBlackFinished()
	{
		SceneManager.LoadScene(nextScene);
	}
}