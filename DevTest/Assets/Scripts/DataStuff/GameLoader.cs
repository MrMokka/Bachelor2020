using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

	public CountdownController Countdown;
	public ScoreController ScoreControllerScript;
	public GameObject StartScreen;
	public List<MinigameController> MinigameControllers = new List<MinigameController>();

	private Dictionary<string, MinigameController> MinigameDict = new Dictionary<string, MinigameController>();

	private QuestionController QController;
	private MinigameController ActiveMinigameController;
	private Question ActiveQuestion;

	void Start() {
		StartScreen.SetActive(true);
		QController = GetComponent<QuestionController>();
		foreach(MinigameController mc in MinigameControllers) {
			//ValidatePanel();
			if(mc == null)
				continue;
			MinigameDict.Add(mc.GetMinigameMode(), mc);
			mc.gameObject.SetActive(false);
			ActiveMinigameController = null;
		}
	}

	public void StartGame() {
		StartScreen.SetActive(false);
		Countdown.SetTime(1000);
		LoadMinigame();
	}

	private void LoadMinigame() {
		//ActiveQuestion = QController.GetRandomQuestion();
		//if(ActiveQuestion == null) {
		//	print("Warning: No Question2 loaded, out of questions?");
		//	GameOver();
		//	return;
		//}
		MinigameController mc;
		if(MinigameDict.TryGetValue("MC", out mc)) {
			//Check for type also
			if(ActiveMinigameController != null)
				ActiveMinigameController.gameObject.SetActive(false);
			ActiveMinigameController = mc;
			ActiveMinigameController.gameObject.SetActive(true);
			NextQuestion();
		}
	}

	public void NextQuestion() {
		ScoreControllerScript.AddScore(ActiveMinigameController.CheckCorrectAnswers());
		StartCoroutine("LoadNextQuestion");
	}

	private IEnumerator LoadNextQuestion() {
		if(ActiveQuestion != null)
			yield return new WaitForSeconds(1f);
		ActiveQuestion = QController.GetRandomQuestion();
		if(ActiveQuestion == null) {
			GameOver();
			yield return null;
		}
		int i = ActiveMinigameController.LoadQuestion(ActiveQuestion);
		ScoreControllerScript.AddAnswersToComplet(i);
	}
	


	public void GameOver() {
		ActiveMinigameController.gameObject.SetActive(false);
		ScoreControllerScript.gameObject.SetActive(true);
		ScoreControllerScript.ShowScore();
		Countdown.StopTimer();
	}


}

[Serializable]
public class MinigamePanel {
	public string Mode;
	public string Type;
	public Transform GameParent;
	public Transform AlternativeParent;
	public Transform QuestionParent;
	public Transform InfoPanel;
	public GameObject AlternativeTemplate;
	public GameObject QuestionTextTemplate;
}