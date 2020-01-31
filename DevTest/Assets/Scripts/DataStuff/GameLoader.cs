using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {
	
	public List<MinigameController> MinigameControllers = new List<MinigameController>();

	private Dictionary<string, MinigameController> MinigameDict = new Dictionary<string, MinigameController>();

	private PrototypeQuestions pq;
	private MinigameController ActiveMinigameController;
	private int questionCounter = 0;
	private Minigame ActiveMinigame;

	void Start() {
		pq = GetComponent<PrototypeQuestions>();
		foreach(MinigameController mc in MinigameControllers) {
			//ValidatePanel();
			if(mc == null)
				continue;
			MinigameDict.Add(mc.GetMinigameMode(), mc);
			mc.gameObject.SetActive(false);
			ActiveMinigameController = null;
		}

		LoadMinigame();
	}

	private void LoadMinigame() {
		ActiveMinigame = pq.GetMinigame();
		if(ActiveMinigame == null) {
			print("Error: No minigame to load");
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
		if(questionCounter >= ActiveMinigame.Questions.Count) {
			LoadMinigame();
		} else {
			ActiveMinigameController.LoadQuestion(ActiveMinigame.Questions[questionCounter]);
			questionCounter++;
		}
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