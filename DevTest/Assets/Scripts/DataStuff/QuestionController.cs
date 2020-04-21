using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour {

	private List<Question> Questions = new List<Question>();
	private List<Question> UsedQuestions = new List<Question>();

	void Awake() {
		
	}

	public void GetQuestionsFromDatabase(int num) {
		DatabaseConnection.ReadQuestionOptions options = new DatabaseConnection.ReadQuestionOptions {
			Number = num,
			IsActive = true
		};
		Questions = DatabaseConnection.ReadQuestionsFromDatabase(options);
	}

	public Question GetRandomQuestion() {
		if(Questions.Count == 0) 
			return null;

		Question Question = Questions[Random.Range(0, Questions.Count)];
		UsedQuestions.Add(Question);
		Questions.Remove(Question);
		//print("Questions left: " + questions.Count);
		return Question;
	}

}
