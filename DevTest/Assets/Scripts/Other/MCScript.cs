using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCScript : MonoBehaviour {

	public List<MCButtonScript> buttons = new List<MCButtonScript>();

	public Text Question2Text;

	private List<MS_Question2> Question2List = new List<MS_Question2>();
	private MS_Question2 activeQuestion2;


	void Start() {
		MS_Question2 q1 = new MS_Question2 {
			Question2Text = "Hvilken er blå?",
			Answers = { "Blå", "Grønn", "Gul", "Rød", "Lilla" }
		};
		q1.RandomizeAnswerPositions();
		AddQuestion2(q1);
		LoadQuestion2(0);
	}





	public void LoadQuestion2(int id = -1) {
		if(Question2List.Count == 0 || id > Question2List.Count) {
			print("Question2 list too small or empty");
			return;
		}
		if(id == -1) {
			id = Random.Range(0, Question2List.Count);
		}
		activeQuestion2 = Question2List[id];

		Question2Text.text = activeQuestion2.Question2Text;
		for(int i = 0; i < buttons.Count; i++) {
			if(i >= activeQuestion2.Answers.Count)
				buttons[i].SetActive(false);
			else
				buttons[i].SetText(activeQuestion2.Answers[i]);
		}

	}


	public void AddQuestion2(MS_Question2 Question2) {
		Question2List.Add(Question2);
	}

	public void AnswerButtonClick(MCButtonScript button) {
		string answer = button.GetText();
		if(answer == activeQuestion2.RightAnswer) {
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
public class MS_Question2 {
	public string Question2Text { get; set; }

	public List<string> Answers = new List<string>();

	public string RightAnswer;

	public MS_Question2() {

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


