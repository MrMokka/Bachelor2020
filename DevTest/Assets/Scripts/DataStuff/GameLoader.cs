using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLoader : MonoBehaviour {

	public CountdownController Countdown;
	public ScoreController ScoreControllerScript;
	public GameObject StartScreen;
	public int QuestionsForMinigameSwap;
	public int MaxQuesitons;

	[Space(5f)]
	public List<MinigameController> MinigameControllers = new List<MinigameController>();


	private List<Minigame> MinigameList = new List<Minigame>();
	private List<Minigame> UsedMinigameList = new List<Minigame>();
	private Minigame ActiveMinigame;

	private QuestionController QController;
	private Question ActiveQuestion;
	private int QuestionCounter;

	private class Minigame {
		public MinigameController Controller;
	}

	void Start() {
		StartScreen.SetActive(true);
		QController = GetComponent<QuestionController>();
		QController.GetQuestionsFromDatabase(MaxQuesitons);
		foreach(MinigameController mc in MinigameControllers) {
			if(mc == null)
				continue;
			mc.gameObject.SetActive(false);
			MinigameList.Add(new Minigame { Controller = mc });
		}
		ActiveMinigame = null;
		QuestionCounter = QuestionsForMinigameSwap;
	}

	public void StartGame() {
		StartScreen.SetActive(false);
		Countdown.SetTime(1000);
		NextQuestion();
	}

	private void LoadMinigame() {
		QuestionCounter = 0;
		if(MinigameList.Count == 0) //Start random pick from used minigames
			return;
		if(ActiveMinigame != null)
			ActiveMinigame.Controller.gameObject.SetActive(false);
		Minigame minigame = MinigameList[Random.Range(0, MinigameList.Count - 1)];
		UsedMinigameList.Add(minigame);
		MinigameList.Remove(minigame);
		ActiveMinigame = minigame;
		ActiveMinigame.Controller.gameObject.SetActive(true);
		ActiveMinigame.Controller.InfoPanel.SetActive(true);
		Countdown.StopTimer();
	}

	public void NextQuestion() {
		if(ActiveMinigame != null)
			ScoreControllerScript.AddScore(ActiveMinigame.Controller.CheckCorrectAnswers());
		if(ActiveMinigame == null || QuestionCounter >= QuestionsForMinigameSwap)
			LoadMinigame();
		QuestionCounter++;
		StartCoroutine("LoadNextQuestion");
	}

	private IEnumerator LoadNextQuestion() {
		if(ActiveQuestion != null)
			yield return new WaitForSeconds(0.6f);
		ActiveQuestion = QController.GetRandomQuestion();
		if(ActiveQuestion == null) {
			GameOver();
			yield return null;
		}
		int i = ActiveMinigame.Controller.LoadQuestion(ActiveQuestion);
		ScoreControllerScript.AddAnswersToComplet(i);
	}

	public void GameOver() {
		ActiveMinigame.Controller.gameObject.SetActive(false);
		ScoreControllerScript.gameObject.SetActive(true);
		ScoreControllerScript.ShowScore();
		Countdown.StopTimer();
	}


}