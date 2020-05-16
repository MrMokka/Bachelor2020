using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionController : MonoBehaviour {

	private List<CategoryQuestions> CategoryQuestionList = new List<CategoryQuestions>();
	private int CategoryIndex, QuestionIndex;

	private class CategoryQuestions {
		public Category Category;
		public List<Question> Questions;
	}

	public void GetQuestionsFromDatabase(int questionsEachCategory) {
		DatabaseConnection.ReadQuestionOptions options = new DatabaseConnection.ReadQuestionOptions {
			Number = questionsEachCategory
		};
		List<Question> questions = DatabaseConnection.ReadQuestionsFromDatabase(options);

		foreach(Question question in questions) {
			CategoryQuestions catQuest = CategoryQuestionList.FirstOrDefault(q => q.Category == question.CategoryList[0]);
			if(catQuest == null) {
				CategoryQuestions catQ = new CategoryQuestions {
					Category = question.CategoryList[0], //TODO: Needs to be fixed for multi category systems
					Questions = new List<Question>()
				};
				catQ.Questions.Add(question);
				CategoryQuestionList.Add(catQ);
			} else {
				catQuest.Questions.Add(question);
			}
		}
		CategoryQuestionList = CategoryQuestionList.OrderBy(x => Random.value).ToList();
	}

	public Question GetQuestion() {
		if(CategoryQuestionList[CategoryIndex].Questions.Count <= QuestionIndex)
			Debug.LogError("IndexError");
		return CategoryQuestionList[CategoryIndex].Questions[QuestionIndex];
	}

	public bool NeedNewMinigame() {
		if(QuestionIndex < CategoryQuestionList[CategoryIndex].Questions.Count - 1) {
			QuestionIndex++;
			return false;
		}
		CategoryIndex++;
		QuestionIndex = 0;
		return true;
	}

	public bool CompletedAllCategories() {
		if(CategoryQuestionList.Count <= CategoryIndex)
			return true;
		return false;
	}

}
