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

	public abstract int LoadQuestion(Question Question);
	public abstract string GetMinigameMode();
	public abstract int CheckCorrectAnswers();
	protected abstract int LoadQuestionLines(List<QuestionLine> questionLines);

}
