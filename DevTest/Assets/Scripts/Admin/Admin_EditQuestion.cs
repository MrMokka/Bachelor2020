using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_EditQuestion : MonoBehaviour {

	public Dropdown DifficultyDropdown, CategoryDropdown;
	public Toggle ActiveToggle;
	public InputField QuestionTextInput;
	public Admin_UpsertQuestion UpsertQuesiton;
	public Admin_SaveFeedbackPanel SaveFeedbackPanel;
	public Admin_EditQuestionPanel EditQuestionPanel;

	[Space(2f)]
	[Header("Question line and alternatives")]
	public GameObject QuestionLinePrefab;
	public Transform QuestionLineParent;
	public GameObject AlternativeLinePrefab;
	public Transform AlternativeParent;
	public Transform QuestionButtonHolder, AlternativeButtonHolder;

	private List<GameObject> QuestionLineList = new List<GameObject>();
	private List<GameObject> AlternativeLineList = new List<GameObject>();
	private List<Dropdown> CorrectAlternativeDropdownList = new List<Dropdown>();
	private List<string> AlternativeTexts = new List<string>();

	private bool SettingStuffUp = false;
	private Question Question;

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
			if(dd.value >= optionData.Count) {
				dd.value = optionData.Count - 1;
			}
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
		CorrectAlternativeDropdownList.Clear();
	}

	public void CleanAlternatives() {
		foreach(GameObject obj in AlternativeLineList) {
			DestroyImmediate(obj);
		}
		AlternativeLineList.Clear();
		AlternativeTexts.Clear();
		UpdateAlternativeDropdowns();
	}

	private void ResetSettings() {
		QuestionTextInput.text = "";
		CleanQuestionLines();
		CleanAlternatives();
		AlternativeTexts.Add("None");
		AddDifficultyDropdown(UpsertQuesiton.DifficultyOptions);
		AddCategoryDropdowns(UpsertQuesiton.Categories);
	}
	#endregion

	public void CorrectAlternativeSelected(Dropdown dropdown) {
		if(SettingStuffUp)
			return;
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

	public void EditQuestion(Question question) {
		Question = question;
		SettingStuffUp = true;
		ResetSettings();
		QuestionObject qObj = question.GetQuestionObject();
		foreach(Alternative alternative in qObj.Alternatives) {
			Admin_AlternativeTemplate altTemplate = Instantiate(AlternativeLinePrefab, AlternativeParent, false).GetComponent<Admin_AlternativeTemplate>();
			AlternativeTexts.Add(alternative.Text);
			altTemplate.AlternativeText.text = alternative.Text;
			AlternativeLineList.Add(altTemplate.gameObject);
			altTemplate.gameObject.SetActive(true);
		}
		foreach(QuestionLine questionLine in qObj.QuestionLines) {
			Admin_QuestionLineTemplate qLine = Instantiate(QuestionLinePrefab, QuestionLineParent, false).GetComponent<Admin_QuestionLineTemplate>();
			qLine.LineText.text = questionLine.Text;
			qLine.CorrectAlternativeDropdown.ClearOptions();
			foreach(string s in AlternativeTexts) {
				qLine.CorrectAlternativeDropdown.options.Add(new Dropdown.OptionData { text = s });
			}
			qLine.CorrectAlternativeDropdown.value = AlternativeTexts.IndexOf(questionLine.CorrectAlternative.Text);
			CorrectAlternativeDropdownList.Add(qLine.CorrectAlternativeDropdown.GetComponent<Dropdown>());
			qLine.gameObject.SetActive(true);
			QuestionLineList.Add(qLine.gameObject);
		}
		AlternativeButtonHolder.SetAsLastSibling();
		QuestionButtonHolder.SetAsLastSibling();
		QuestionTextInput.text = question.QuestionText;
		//WARNING: Need fix for non singlular category searches (only capeable of working with a single category atm)
		CategoryDropdown.value = CategoryDropdown.options.FindIndex(i => i.text.Equals(question.CategoryList[0].Name));
		CategoryDropdown.RefreshShownValue();
		DifficultyDropdown.value = DifficultyDropdown.options.FindIndex(i => i.text.Equals(question.Weight.ToString()));
		DifficultyDropdown.RefreshShownValue();
		ActiveToggle.isOn = Convert.ToBoolean(Question.Active);
		SettingStuffUp = false;
	}
	
	public void SaveUpdatedQuestion() {
		string jsonString = JsonUtility.ToJson(CreateQuestionObject());
		Question question = new Question {
			Id = Question.Id,
			Active = Convert.ToInt32(ActiveToggle.isOn),
			CategoryList = new List<Category> { new Category { Name = CategoryDropdown.options[CategoryDropdown.value].text } },
			QuestionObject = jsonString,
			QuestionText = QuestionTextInput.text,
			Type = new Type { Name = "MC" },
			Weight = DifficultyDropdown.value + 1
		};
		SaveFeedbackPanel.gameObject.SetActive(true);
		SaveFeedbackPanel.ShowText(DatabaseConnection.UpdateQuestionInDatabase(question));
	}
	
	private QuestionObject CreateQuestionObject() {
		List<Alternative> alternatives = new List<Alternative>();
		foreach(string alternative in AlternativeTexts) {
			alternatives.Add(new Alternative {
				Text = alternative
			});
		}
		alternatives.RemoveAt(0);
		List<QuestionLine> questionLines = new List<QuestionLine>();
		foreach(GameObject questionLine in QuestionLineList) {
			Dropdown correctAlternativeDropdown = questionLine.transform.GetChild(1).GetComponent<Dropdown>();
			QuestionLine ql = new QuestionLine {
				Text = questionLine.transform.GetChild(0).GetComponent<InputField>().text
			};
			//print(correctAlternativeDropdown.value + " : " + alternatives[correctAlternativeDropdown.value].Text);
			if(correctAlternativeDropdown.value == 0)
				ql.CorrectAlternative = null;
			else
				ql.CorrectAlternative = alternatives[correctAlternativeDropdown.value - 1];
			questionLines.Add(ql);
		}
		QuestionObject qObj = new QuestionObject {
			Alternatives = alternatives,
			QuestionLines = questionLines
		};
		return qObj;
	}
	
	public void CloseEditWindow() {
		EditQuestionPanel.SearchQuestions();
		gameObject.SetActive(false);
	}
	

}
