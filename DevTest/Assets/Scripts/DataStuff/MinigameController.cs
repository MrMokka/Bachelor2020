using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour {

	[Serializable]
	protected class QuestionField<T> {
		public GameObject LineObj;
		public QuestionLine Line;
		public T Script;
	}

	public GameObject InfoPanel;
	protected Question Question;
	protected int TotalAnswers;

	public abstract void LoadQuestion(Question Question);
	public abstract string GetMinigameMode();
	public abstract ScoreController.QuestionScore CheckCorrectAnswers();
	protected abstract void LoadQuestionLines(List<QuestionLine> questionLines);

}
