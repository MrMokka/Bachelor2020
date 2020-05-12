using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	private float TotalScore;
	private int TotalMaxScore;

	private List<Question> AnsweredQuestions = new List<Question>();

	public class QuestionScore {
		public Question Question;
		public float Points;
		public int MaxPoints;
	}

	public void AddQuestionPoints(QuestionScore questionScore) {
		questionScore.Question.Score = questionScore.Points * questionScore.Question.Weight;
		questionScore.Question.MaxScore = questionScore.MaxPoints * questionScore.Question.Weight;
		AnsweredQuestions.Add(questionScore.Question);
		TotalScore += questionScore.Question.Score;
		TotalMaxScore += questionScore.Question.MaxScore;
	}

	public List<Question> GetAnsweredQuestions() { return AnsweredQuestions; }

	public float GetTotalScore() {
		return TotalScore;
	}

	public int GetMaxScore() {
		return TotalMaxScore;
	}

	

}
