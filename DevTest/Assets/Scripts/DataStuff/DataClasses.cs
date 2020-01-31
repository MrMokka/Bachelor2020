using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClasses {
	
	private Dictionary<string, Minigame> MinigameDict = new Dictionary<string, Minigame>();

	public List<Minigame> Minigames = new List<Minigame>();

	private List<Minigame> UsedMinigames = new List<Minigame>();


	public Minigame GetRandomMinigame() {
		if(Minigames.Count == 0) {
			return null;
		}
		Minigame minigame = Minigames[Random.Range(0, Minigames.Count)];
		UsedMinigames.Add(minigame);
		Minigames.Remove(minigame);
		return minigame;
	}

	public Minigame CreateNewMinigame(string mode, string type) {
		Minigame m = new Minigame {
			Mode = mode,
			Type = type
		};
		MinigameDict.Add(mode, m);
		Minigames.Add(m);
		return m;
	}

	public Minigame GetMinigame(string mode) {
		MinigameDict.TryGetValue(mode, out Minigame m);
		return m;
	}


}

public class Minigame {
	public List<string> Categories = new List<string>();
	public string Type;
	public string Mode;
	public List<Question> Questions = new List<Question>();
	public void AddQuestions(string questionText, List<string> questionLines, List<string> answers, List<int> correctAnswers) {
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
					interactable = false,
					correctAnswer = null
				});
			}

		}
		q.grading = false;
		Questions.Add(q);
	}
}

public class Question {
	public string QuestionText;
	public List<Answer> Answers = new List<Answer>();
	public List<TextLine> TextLines = new List<TextLine>();
	public bool grading;
}

public class Answer {
	public string text;
}

public class TextLine {
	public string text;
	public bool interactable;
	public CorrectAnswer correctAnswer;
}

public class CorrectAnswer {
	public Answer answer;
}
