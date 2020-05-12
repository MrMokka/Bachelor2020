using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataClasses {

	/*
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
	*/

}

#region Old Classes
/*
[Serializable]
public class Minigame {
	public List<string> Categories = new List<string>();
	public string Type;
	public string Mode;
	public List<Question2> Questions = new List<Question2>();
	public void AddQuestions(string questionText, List<string> questionLines, List<string> answers, List<int> correctAnswers) {
		Question2 q = new Question2 {
			QuestionText = questionText
		};
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

[Serializable]
public class Question2 {
	public string QuestionText;
	public List<Answer> Answers = new List<Answer>();
	public List<TextLine> TextLines = new List<TextLine>();
	public bool grading;
}

[Serializable]
public class Answer {
	public string text;
}

[Serializable]
public class TextLine {
	public string text;
	public bool interactable;
	public CorrectAnswer correctAnswer;
}

[Serializable]
public class CorrectAnswer {
	public Answer answer;
}
*/
#endregion

[Serializable]
public class Question {
	public int Id;
	public Type Type;
	public string QuestionText;
	public List<Category> CategoryList = new List<Category>();
	public string QuestionObject;
	public int Weight;
	public int Active;
	public float Score;
	public int MaxScore; //Score calculated with weight

	public QuestionObject GetQuestionObject() {
		return JsonUtility.FromJson<QuestionObject>(QuestionObject);
	}
}

[Serializable]
public class Type {
	public int Id;
	public string Name;
}

[Serializable]
public class Category {
	public int Id;
	public string Name;
}

[Serializable]
public class QuestionObject {
	public List<QuestionLine> QuestionLines = new List<QuestionLine>();
	public List<Alternative> Alternatives = new List<Alternative>();

}

[Serializable]
public class QuestionLine {
	public string Text;
	public Alternative CorrectAlternative;
}

[Serializable]
public class Alternative {
	public string Text;
}


