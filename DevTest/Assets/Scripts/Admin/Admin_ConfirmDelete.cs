using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_ConfirmDelete : MonoBehaviour {

	public Admin_EditQuestionPanel EditQuestionPanel;
	public Admin_SaveFeedbackPanel DeleteFeedbackPanel;

	private Admin_QuestionLineEdit QuestionToDelete;

	void Awake() {
		if(transform.GetChild(0).gameObject.activeSelf)
			transform.GetChild(0).gameObject.SetActive(false);
	}


	public void ConfirmDelete(Admin_QuestionLineEdit lineEdit) {
		QuestionToDelete = lineEdit;
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void ConfirmButton() {
		transform.GetChild(0).gameObject.SetActive(false);
		bool result = QuestionToDelete.DeleteQuestion();
		if(result) {
			EditQuestionPanel.DeleteQuestionLine(QuestionToDelete);
		} else {
			print("Something went wrong deleting from database");
		}
		DeleteFeedbackPanel.ShowText(result);
	}

	public void DeclineButton() {
		transform.GetChild(0).gameObject.SetActive(false);
		QuestionToDelete = null;
	}


}
