using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

	public TimeController TCS;
	public ScoreController SC;
	public GameObject StartScreen;
	public List<MinigameController> MinigameControllers = new List<MinigameController>();

	private Dictionary<string, MinigameController> MinigameDict = new Dictionary<string, MinigameController>();

	private PrototypeQuestions pq;
	private MinigameController ActiveMinigameController;
	private int questionCounter = 0;
	private Minigame ActiveMinigame;

	void Start() {
		StartScreen.SetActive(true);
		pq = GetComponent<PrototypeQuestions>();
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
		TCS.SetTime(10);
		LoadMinigame();
	}

	private void LoadMinigame() {
		ActiveMinigame = pq.GetMinigame();
		if(ActiveMinigame == null) {
			print("Warning: No minigame loaded, out of minigames?");
			GameOver();
			return;
		}
		MinigameController mc;
		if(MinigameDict.TryGetValue(ActiveMinigame.Mode, out mc)) {
			//Check for type also
			if(ActiveMinigameController != null)
				ActiveMinigameController.gameObject.SetActive(false);
			ActiveMinigameController = mc;
			ActiveMinigameController.gameObject.SetActive(true);
			questionCounter = 0;
			NextQuestion();
		}
	}

	public void NextQuestion() {
		SC.AddScore(ActiveMinigameController.CheckCorrectAnswers());
		if(ActiveMinigame == null)
			print("ERROR: No active minigame loaded!");
		if(questionCounter >= ActiveMinigame.Questions.Count) {
			LoadMinigame();
		} else {
			int i = ActiveMinigameController.LoadQuestion(ActiveMinigame.Questions[questionCounter]);
			SC.AddAnswersToComplet(i);
			questionCounter++;
		}
	}


	public void GameOver() {
		ActiveMinigameController.gameObject.SetActive(false);
		SC.gameObject.SetActive(true);
		SC.ShowScore();
		TCS.StopTimer();
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