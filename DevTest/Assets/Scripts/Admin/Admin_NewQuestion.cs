using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_NewQuestion : MonoBehaviour {

	public Dropdown Difficulty, Category;
	public InputField QuestionTextInput;
	public Admin_LineController LineController;
	public string QuestionType;


	void Awake() {
		Difficulty.ClearOptions();
		for(int i = 1; i <= 5; i++) {
			Difficulty.options.Add(new Dropdown.OptionData {
				text = i.ToString()
			});
		}
		UpdateDropdowns();
	}


	public void UpdateDropdowns() {
		int index = Category.value;
		Category.ClearOptions();
		foreach(Category cat in DatabaseConnection.GetCategories()) {
			Category.options.Add(new Dropdown.OptionData { text = cat.Name });
		}
		Category.value = index;
	}

	public void SaveQuestion() {
		string jsonString = JsonUtility.ToJson(CreateQuestionObject());
		Question question = new Question {
			Active = 1,
			CategoryList = new List<Category> { new Category { Name = Category.options[Category.value].text } },
			QuestionObject = jsonString,
			QuestionText = QuestionTextInput.text,
			Type = new Type { Name = QuestionType },
			Weight = Difficulty.value + 1
		};
		DatabaseConnection.WriteQuestionToDatabase(question);
		ResetFields();
	}

	private QuestionObject CreateQuestionObject() {
		List<Alternative> alternatives = new List<Alternative>();
		LineController.AlternativeTexts.RemoveAt(0);
		foreach(string alternative in LineController.AlternativeTexts) {
			alternatives.Add(new Alternative {
				Text = alternative
			});
		}
		List<QuestionLine> questionLines = new List<QuestionLine>();
		foreach(GameObject questionLine in LineController.QuestionLineList) {
			Dropdown correctAlternativeDropdown = questionLine.transform.GetChild(1).GetComponent<Dropdown>();
			QuestionLine ql = new QuestionLine {
				Text = questionLine.transform.GetChild(0).GetComponent<InputField>().text
			};
			if(correctAlternativeDropdown.value == 0) {
				ql.CorrectAlternative = null;
			} else {
				ql.CorrectAlternative = alternatives[correctAlternativeDropdown.value - 1];
			}
			questionLines.Add(ql);
		}

		QuestionObject qObj = new QuestionObject {
			Alternatives = alternatives,
			QuestionLines = questionLines
		};

		return qObj;
	}


	public void ResetFields() {
		QuestionTextInput.text = "";
		LineController.CleanAlternatives();
		LineController.CleanQuestionLines();
		LineController.AlternativeTexts.Add("None");
	}


}
