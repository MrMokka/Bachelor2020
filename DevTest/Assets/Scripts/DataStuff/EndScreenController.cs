using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

	public InputField EmailInput;
	public Text ScoreText;
	public ScoreController ScoreController;
	public GameLoader GameLoader;
	public GameObject ScoreScreen, ThanksScreen;



	public void SubmitScore() {
		GameLoader.SubmitScore(EmailInput.text);
		ScoreScreen.SetActive(false);
		ThanksScreen.SetActive(true);
	}

	public void ShowScore() {
		ScoreScreen.SetActive(true);
		ScoreText.text = string.Format(ScoreText.text, ScoreController.GetTotalScore());
	}

}
