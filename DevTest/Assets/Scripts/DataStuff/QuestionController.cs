using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour {

	private List<Question> questions = new List<Question>();
	private List<Question> usedQuestions = new List<Question>();

	void Awake() {
		questions = DatabaseConnection.ReadQuestionsFromDatabase("Simple Math");
		print(questions.Count);
	}

	public Question GetRandomQuestion() {
		if(questions.Count == 0) 
			return null;

		Question Question = questions[Random.Range(0, questions.Count)];
		usedQuestions.Add(Question);
		questions.Remove(Question);
		print("Questions left: " + questions.Count);
		return Question;
	}

}
