using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_NewCategory : MonoBehaviour {

	public InputField Inputfield;
	public Admin_NewQuestion NewQuestionScript;


	public void SaveCategory() {
		Category category = new Category {
			Name = Inputfield.text
		};
		DatabaseConnection.WriteCategoryToDatabase(category);
		Inputfield.text = "";
		NewQuestionScript.UpdateDropdowns();
	}


}
