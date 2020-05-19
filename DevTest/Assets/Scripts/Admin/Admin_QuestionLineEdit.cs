using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_QuestionLineEdit : MonoBehaviour {

	public Text IdText, QuestionText, DifficultyText, CategoryText;
	public Admin_ConfirmDelete ConfirmDelete;
	public Admin_EditQuestion EditQuestionPanel;

	private Question Question;


	public void SetQuestion(Question question) {
		Question = question;
		IdText.text = question.Id.ToString();
		QuestionText.text = question.QuestionText;
		DifficultyText.text = question.Weight.ToString();
		CategoryText.text = question.CategoryList[0].Name;
	}

	public void EditQuestion() {
		EditQuestionPanel.gameObject.SetActive(true);
		EditQuestionPanel.EditQuestion(Question);
	}

	public void ConfirmDeleteQuestion() {
		ConfirmDelete.ConfirmDelete(this);
	}

	public bool DeleteQuestion() {
		return DatabaseConnection.DeleteQuestionInDatabase(Question);
	}

}
