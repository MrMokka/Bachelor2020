using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour {

	private List<Question> questions = new List<Question>();
	private List<Question> usedQuestions = new List<Question>();

	void Awake() {
		questions = DatabaseConnection.ReadQuestionsFromDatabase("Casual Speed");
		print(questions.Count);
	}

	public Question GetRandomQuestion() {
		if(questions.Count == 0) 
			return null;

		Question Question2 = questions[Random.Range(0, questions.Count)];
		print(Question2);
		usedQuestions.Add(Question2);
		questions.Remove(Question2);
		return Question2;
	}

}
