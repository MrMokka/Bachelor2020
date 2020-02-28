using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text ScoreText;

	private int Score;
	private int AnswersCompleted;

	public void AddScore(int i) {
		Score += i;
	}

	public void AddAnswersToComplet(int i) {
		AnswersCompleted += i;
	}

	public void ShowScore() {
		ScoreText.text = string.Format(ScoreText.text, Score, AnswersCompleted);
	}

}
