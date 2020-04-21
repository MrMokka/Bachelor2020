using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_EditQuestion : MonoBehaviour {

	public Dropdown DifficultyDropdown, CategoryDropdown;
	public InputField QuestionTextInput;
	public Admin_UpsertQuestion UpsertQuesiton;

	[Space(2f)]
	[Header("Question line and alternatives")]
	public GameObject QuestionLinePrefab;
	public Transform QuestionLineParent;
	public GameObject AlternativeLinePrefab;
	public Transform AlternativeParent;

	private List<GameObject> QuestionLineList = new List<GameObject>();
	private List<GameObject> AlternativeLineList = new List<GameObject>();
	private List<Dropdown> CorrectAlternativeDropdownList = new List<Dropdown>();
	private List<string> AlternativeTexts = new List<string>();

	#region hidefornow

	public void OnOpen() {
		ResetFields();
		AddDifficultyDropdown(UpsertQuesiton.DifficultyOptions);
		AddCategoryDropdowns(UpsertQuesiton.Categories);
	}

	public void AddDifficultyDropdown(int[] difficulties) {
		DifficultyDropdown.ClearOptions();
		foreach(int i in difficulties) {
			DifficultyDropdown.options.Add(new Dropdown.OptionData {
				text = i.ToString()
			});
		}
	}


	public void AddCategoryDropdowns(List<Category> categories) {
		CategoryDropdown.ClearOptions();
		foreach(Category cat in categories) {
			CategoryDropdown.options.Add(new Dropdown.OptionData { text = cat.Name });
		}
	}

	#region QuestionLine
	public void AddQuestionLine() {
		if(QuestionLineList.Count > 0 &&
			QuestionLineList[QuestionLineList.Count - 1].transform.GetChild(0).GetComponent<InputField>().text == "")
			return;
		Transform buttonHolder = QuestionLineParent.GetChild(QuestionLineParent.childCount - 1);
		GameObject questionLine = Instantiate(QuestionLinePrefab, QuestionLineParent, false);
		questionLine.SetActive(true);
		QuestionLineList.Add(questionLine);
		buttonHolder.SetAsLastSibling();
		CorrectAlternativeDropdownList.Add(questionLine.transform.GetChild(1).GetComponent<Dropdown>());
		UpdateAlternativeDropdowns();
	}

	public void RemoveQuestionLine() {
		if(QuestionLineList.Count > 0) {
			GameObject toDelete = QuestionLineList[QuestionLineList.Count - 1];
			QuestionLineList.Remove(toDelete);
			DestroyImmediate(toDelete);
		}
	}
	#endregion

	#region AlternativeLine
	public void AddAlternativeLine() {
		if(AlternativeLineList.Count > 0 &&
			AlternativeLineList[AlternativeLineList.Count - 1].transform.GetChild(0).GetComponent<InputField>().text == "")
			return;
		Transform buttonHolder = AlternativeParent.GetChild(AlternativeParent.childCount - 1);
		GameObject alternativeLine = Instantiate(AlternativeLinePrefab, AlternativeParent, false);
		alternativeLine.SetActive(true);
		AlternativeLineList.Add(alternativeLine);
		buttonHolder.SetAsLastSibling();
		AlternativeTexts.Add("");
	}

	public void RemoveAlternativeLine() {
		if(AlternativeLineList.Count > 0) {
			GameObject toDelete = AlternativeLineList[AlternativeLineList.Count - 1];
			AlternativeLineList.Remove(toDelete);
			DestroyImmediate(toDelete);
			AlternativeTexts.RemoveAt(AlternativeTexts.Count - 1);
			UpdateAlternativeDropdowns();
		}
	}
	#endregion

	#region Updates
	private void UpdateAlternativeDropdowns() {
		List<Dropdown.OptionData> optionData = new List<Dropdown.OptionData>();
		foreach(string s in AlternativeTexts) {
			optionData.Add(new Dropdown.OptionData {
				text = s
			});
		}
		foreach(Dropdown dd in CorrectAlternativeDropdownList) {
			dd.options = optionData;
		}
	}
	public void UpdateAlternativeTexts(InputField alternative) {
		int index = AlternativeLineList.IndexOf(alternative.transform.parent.gameObject);
		if(index + 1 >= AlternativeTexts.Count)
			AlternativeTexts.Add(alternative.text);
		else
			AlternativeTexts[index + 1] = alternative.text;
		UpdateAlternativeDropdowns();
	}
	#endregion

	#region Cleaning
	public void CleanQuestionLines() {
		foreach(GameObject obj in QuestionLineList) {
			DestroyImmediate(obj);
		}
		QuestionLineList.Clear();
	}

	public void CleanAlternatives() {
		foreach(GameObject obj in AlternativeLineList) {
			DestroyImmediate(obj);
		}
		AlternativeLineList.Clear();
		AlternativeTexts.Clear();
		UpdateAlternativeDropdowns();
	}

	public void ResetFields() {
		QuestionTextInput.text = "";
		CleanQuestionLines();
		CleanAlternatives();
		AlternativeTexts.Add("None");
	}
	#endregion

	public void CorrectAlternativeselected(Dropdown dropdown) {
		InputField inputField = dropdown.transform.parent.GetChild(0).GetComponent<InputField>();
		if(dropdown.value == 0 && inputField.text.Contains("{0}")) {
			inputField.text = inputField.text.Replace("{0}", "");
		} else if(!inputField.text.Contains("{0}")) {
			if(inputField.text[inputField.text.Length - 1] == ' ')
				inputField.text += "{0}";
			else
				inputField.text += " {0}";
		}
		string str = inputField.text.Substring(0, inputField.text.IndexOf("{0}") + "{0}".Length);
		inputField.text = str + inputField.text.Substring(str.Length).Replace("{0}", "");

		//TODO: Remove unused space at end of inputField.text
		//if(inputField.text[inputField.text.Length -1].ToString() == " ")
		//	inputField.text.Remove(inputField.text.Length - 1);
	}
	#endregion

	public void SaveQuestionEdit() {
		//Turn save button green/red

	}
	
	public void SaveQuestion() {
		string jsonString = JsonUtility.ToJson(CreateQuestionObject());
		Question question = new Question {
			Active = 1,
			CategoryList = new List<Category> { new Category { Name = CategoryDropdown.options[CategoryDropdown.value].text } },
			QuestionObject = jsonString,
			QuestionText = QuestionTextInput.text,
			Type = new Type { Name = "MC" },
			Weight = DifficultyDropdown.value + 1
		};
		DatabaseConnection.WriteQuestionToDatabase(question);
		ResetFields();
	}
	
	private QuestionObject CreateQuestionObject() {
		List<Alternative> alternatives = new List<Alternative>();
		AlternativeTexts.RemoveAt(0);
		foreach(string alternative in AlternativeTexts) {
			alternatives.Add(new Alternative {
				Text = alternative
			});
		}
		List<QuestionLine> questionLines = new List<QuestionLine>();
		foreach(GameObject questionLine in QuestionLineList) {
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
	
	

}
