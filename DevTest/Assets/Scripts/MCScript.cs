using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCScript : MonoBehaviour {

	public List<MCButtonScript> buttons = new List<MCButtonScript>();

	public Text questionText;

	private List<MS_Question> questionList = new List<MS_Question>();
	private MS_Question activeQuestion;


	void Start() {
		MS_Question q1 = new MS_Question {
			QuestionText = "Hvilken er blå?",
			Answers = { "Blå", "Grønn", "Gul", "Rød", "Lilla" }
		};
		q1.RandomizeAnswerPositions();
		AddQuestion(q1);
		LoadQuestion(0);
	}





	public void LoadQuestion(int id = -1) {
		if(questionList.Count == 0 || id > questionList.Count) {
			print("Question list too small or empty");
			return;
		}
		if(id == -1) {
			id = Random.Range(0, questionList.Count);
		}
		activeQuestion = questionList[id];

		questionText.text = activeQuestion.QuestionText;
		for(int i = 0; i < buttons.Count; i++) {
			if(i >= activeQuestion.Answers.Count)
				buttons[i].SetActive(false);
			else
				buttons[i].SetText(activeQuestion.Answers[i]);
		}

	}


	public void AddQuestion(MS_Question question) {
		questionList.Add(question);
	}

	public void AnswerButtonClick(MCButtonScript button) {
		string answer = button.GetText();
		if(answer == activeQuestion.RightAnswer) {
			button.SetColor(Color.green);
		} else {
			button.SetColor(Color.red);
		}
	}

	public static void PrintText(string txt) {
		print(txt);
	}
}


/// <summary>
/// First answer is the correct answer.
/// </summary>
public class MS_Question {
	public string QuestionText { get; set; }

	public List<string> Answers = new List<string>();

	public string RightAnswer;

	public MS_Question() {

	}
	public void RandomizeAnswerPositions() {
		RightAnswer = Answers[0];

		List<string> newAnswers = new List<string>();
		foreach(string s in Answers) {
			int i = Random.Range(0, newAnswers.Count);
			MCScript.PrintText(i.ToString());
			newAnswers.Insert(i, s);
		}
		Answers = newAnswers;

		//answers.Add(AnswerA, AnswerB, AnswerC, AnswerD);

	}

}


