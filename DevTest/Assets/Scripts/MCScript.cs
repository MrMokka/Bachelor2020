using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCScript : MonoBehaviour {

	public List<MCButtonScript> buttons = new List<MCButtonScript>();

	public Text questionText;

	private List<Question> questionList = new List<Question>();
	private Question activeQuestion;


	void Start() {
		Question q1 = new Question {
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


	public void AddQuestion(Question question) {
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
public class Question {
	public string QuestionText { get; set; }

	public List<string> Answers = new List<string>();

	public string RightAnswer;

	public Question() {

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


