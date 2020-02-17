using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_LineController : MonoBehaviour {

	public GameObject QuestionLinePrefab, AlternativeLinePrefab;
	public Transform QuesitonLineParent, AlternativeLineParent;
	public List<GameObject> QuestionLineList = new List<GameObject>();
	public List<GameObject> AlternativeLineList = new List<GameObject>();

	public List<string> AlternativeTexts = new List<string>();

	private List<Dropdown> CorrectAlternativeDropdownList = new List<Dropdown>();

	void Awake() {
		AlternativeTexts.Add("None");
	}

	public void AddQuestionLine() {
		if(QuestionLineList.Count > 0 &&
			QuestionLineList[QuestionLineList.Count - 1].transform.GetChild(0).GetComponent<InputField>().text == "")
			return;
		Transform buttonHolder = QuesitonLineParent.GetChild(QuesitonLineParent.childCount - 1);
		GameObject questionLine = Instantiate(QuestionLinePrefab, QuesitonLineParent, false);
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

	public void AddAlternativeLine() {
		if(AlternativeLineList.Count > 0 && 
			AlternativeLineList[AlternativeLineList.Count - 1].transform.GetChild(0).GetComponent<InputField>().text == "")
			return;
		Transform buttonHolder = AlternativeLineParent.GetChild(AlternativeLineParent.childCount - 1);
		GameObject alternativeLine = Instantiate(AlternativeLinePrefab, AlternativeLineParent, false);
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

	public void CorrectAlternativeselected(Dropdown dropdown) {
		InputField inputField = dropdown.transform.parent.GetChild(0).GetComponent<InputField>();
		if(dropdown.value == 0 && inputField.text.Contains("{0}")) {
			
			inputField.text = inputField.text.Replace("{0}", "");
			print("Replaced");
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


}
