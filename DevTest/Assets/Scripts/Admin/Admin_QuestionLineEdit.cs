using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_QuestionLineEdit : MonoBehaviour {

	public Text Text;
	public Admin_ConfirmDelete ConfirmDelete;
	public Admin_EditQuestion EditQuestionPanel;

	private Question Question;


	public void SetQuestion(Question question) {
		Question = question;
		Text.text = question.QuestionText;
	}

	public void EditQuestion() {
		EditQuestionPanel.gameObject.SetActive(true);
		EditQuestionPanel.EditQuestion(Question);
	}

	public void ConfirmDeleteQuestion() {
		ConfirmDelete.ConfirmDelete(this);
	}

}
