using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeQuestions : MonoBehaviour {

	private DataClasses dc;

	void Awake() {
		dc = new DataClasses();
		Minigame m = dc.CreateNewMinigame("DnD", "Fill");
		m.AddQuestions("Calculate",
					new List<string> { "10 + 10 = {0}" },
					new List<string> { "56", "105", "20", "0" },
					new List<int> { 2 });
		m.AddQuestions("How many items will this loop return?",
					new List<string> {
						"Answer: {0}",
						"for(int i = 0; i < 10; i++) {",
						"	if(i % 2 == 0) return i;",
						"}"
					},
					new List<string> { "3", "4", "5", "6", "7" },
					new List<int> { 2 });
	}

	private void AddQuestions_Remove(string questionText, List<string> questionLines, List<string> answers, List<int> correctAnswers) {
		Minigame mg = new Minigame();
		mg.Mode = "DnD";
		mg.Type = "Fill";

		Question q = new Question();
		q.QuestionText = questionText;
		foreach(string s in answers) {
			q.Answers.Add(new Answer { text = s });
		}
		int i = 0;
		foreach(string ql in questionLines) {
			if(ql.Contains("{0}")) {
				q.TextLines.Add(new TextLine {
					text = ql,
					interactable = true,
					correctAnswer = new CorrectAnswer { answer = q.Answers[correctAnswers[i]] }
				});
				i++;
			} else {
				q.TextLines.Add(new TextLine {
					text = ql,
					interactable = true,
					correctAnswer = null
				});
			}
			
		}
		q.grading = false;
		mg.Questions.Add(q);
		dc.Minigames.Add(mg);
	}

	public Minigame GetMinigame() {
		return dc.GetRandomMinigame();
	}

}
