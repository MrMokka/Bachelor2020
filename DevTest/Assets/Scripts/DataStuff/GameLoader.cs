using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLoader : MonoBehaviour {

	public float NextQuestionDelay;

	[Space(3f)]
	public CountdownController Countdown;
	public ScoreController ScoreControllerScript;
	public EndScreenController EndScreenController;
	public GameObject StartScreen;
	public int QuestionsForCategory;

	[Space(5f)]
	public List<MinigameController> MinigameControllers = new List<MinigameController>();


	[Header("DEBUG")]
	public string Email;
	public bool UseDebugEmail;

	private float NextQuestionDelayTimer = 0;

	private List<Minigame> MinigameList = new List<Minigame>();
	private List<Minigame> UsedMinigameList = new List<Minigame>();
	private Minigame ActiveMinigame;

	private QuestionController QController;
	private Question ActiveQuestion;

	private class Minigame {
		public MinigameController Controller;
	}

	void Start() {
		StartScreen.SetActive(true);
		QController = GetComponent<QuestionController>();
		QController.GetQuestionsFromDatabase(QuestionsForCategory);
		foreach(MinigameController mc in MinigameControllers) {
			if(mc == null)
				continue;
			mc.gameObject.SetActive(false);
			MinigameList.Add(new Minigame { Controller = mc });
		}
		ActiveMinigame = null;
	}

	public void StartGame() {
		StartScreen.SetActive(false);
		Countdown.SetTime(900);
		LoadMinigame();
		CheckIfNeedMinigame(true);
	}

	void Update() {
		if(NextQuestionDelayTimer > 0)
			NextQuestionDelayTimer -= Time.deltaTime;
	}

	private void LoadMinigame() {
		if(MinigameList.Count == 0) //Start random pick from used minigames
			return;
		if(ActiveMinigame != null)
			ActiveMinigame.Controller.gameObject.SetActive(false);
		Minigame minigame = MinigameList[Random.Range(0, MinigameList.Count)];
		UsedMinigameList.Add(minigame);
		MinigameList.Remove(minigame);
		ActiveMinigame = minigame;
		ActiveMinigame.Controller.gameObject.SetActive(true);
		ActiveMinigame.Controller.InfoPanel.SetActive(true);
		Countdown.StopTimer();
		//StartCoroutine("StopTimescaleAfterDelay", 1f);
	}

	private IEnumerator StopTimescaleAfterDelay(float delay) {
		Countdown.StopTimer();
		Countdown.SetTimeScale(1);
		yield return new WaitForSeconds(delay);
		Countdown.ResumeTimer();
	}

	public void NextQuestion(bool skipAnswerCheck = false) {
		UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		if(NextQuestionDelayTimer > 0)
			return;
		NextQuestionDelayTimer = NextQuestionDelay;
		if(ActiveMinigame != null && !skipAnswerCheck)
			ScoreControllerScript.AddQuestionPoints(ActiveMinigame.Controller.CheckCorrectAnswers());
		CheckIfNeedMinigame();
	}

	public void CheckIfNeedMinigame(bool skipMinigameCheck = false) {
		if(!skipMinigameCheck && QController.NeedNewMinigame()) {
			if(QController.CompletedAllCategories())
				StartCoroutine("GameOverAfterDelay");
			else
				StartCoroutine("LoadNewMinigameAfterDelay");
		} else {
			StartCoroutine("LoadNextQuestion");
		}
	}

	private IEnumerator GameOverAfterDelay() {
		yield return new WaitForSeconds(0.6f);
		GameOver();
	}

	private IEnumerator LoadNewMinigameAfterDelay() {
		yield return new WaitForSeconds(0.6f);
		LoadMinigame();
		StartCoroutine("LoadNextQuestion");
	}

	private IEnumerator LoadNextQuestion() {
		if(ActiveQuestion != null)
			yield return new WaitForSeconds(0.6f);
		ActiveQuestion = QController.GetQuestion();
		print("Active Question: " + ActiveQuestion);
		ActiveMinigame.Controller.LoadQuestion(ActiveQuestion);
	}

	public void GameOver() {
		ActiveMinigame.Controller.gameObject.SetActive(false);
		EndScreenController.gameObject.SetActive(true);
		EndScreenController.ShowScore();
		Countdown.StopTimerWithoutScale();
	}

	public void SubmitScore(string email) {
		if(UseDebugEmail) {
			email = Email;
		}
		DatabaseConnection.WriteScoreToDatabase(ScoreControllerScript.GetAnsweredQuestions(), ScoreControllerScript.GetTotalScore(), email);
	}

}
