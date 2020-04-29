using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_ConfirmDelete : MonoBehaviour {

	public Admin_EditQuestionPanel EditQuestionPanel;

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
		EditQuestionPanel.DeleteQuestionLine(QuestionToDelete);
	}

	public void DeclineButton() {
		transform.GetChild(0).gameObject.SetActive(false);
		QuestionToDelete = null;
	}


}
