using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_ViewScoreLine : MonoBehaviour {

	public Text IdText, CategoryText, QuestionTextText, ScoreText, MaxScoreText;

	private Score Score;

	public void SetInformation(Score score) {
		Score = score;
		IdText.text = score.Id.ToString();
		CategoryText.text = score.ScoreQuestion.Category.Name;
		QuestionTextText.text = score.ScoreQuestion.QuestionText;
		ScoreText.text = score.QuestionScore.ToString();
		MaxScoreText.text = score.MaxScore.ToString();
	}



}
