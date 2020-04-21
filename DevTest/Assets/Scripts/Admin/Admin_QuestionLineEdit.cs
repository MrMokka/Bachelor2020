using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_QuestionLineEdit : MonoBehaviour {

	public Text Text;
	public Admin_ConfirmDelete ConfirmDelete;

	private Question Question;


	public void SetQuestion(Question question) {
		Question = question;
		Text.text = question.QuestionText;
	}

	public void EditQuestion() {

	}

	public void ConfirmDeleteQuestion() {
		ConfirmDelete.ConfirmDelete(this);
	}

}
