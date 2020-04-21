using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_EditQuestionPanel : MonoBehaviour {

	public Transform QuestionParent;
	public Dropdown DifficultyDropdown, CategoryDropdown;
	public Admin_QuestionLineEdit QuestionLine;
	public Admin_UpsertQuestion UpsertQuesiton;

	private List<Admin_QuestionLineEdit> QuestionLineList = new List<Admin_QuestionLineEdit>();

	private List<int> DifficultysSelected = new List<int>();

	public void OnOpen() {
		UpsertQuesiton.UpdateCategories();
		UpdateDifficultyDropdown(UpsertQuesiton.DifficultyOptions);
		UpdateCategoryDropdowns(UpsertQuesiton.Categories);
		UpsertQuesiton.SearchQuestions();
	}

	public string GetSelectedDifficulty() { return DifficultyDropdown.options[DifficultyDropdown.value].text; }
	public string GetSelectedCategory() { return CategoryDropdown.options[CategoryDropdown.value].text; }

	public void RespawnQuestions(List<Question> questions) {
		ClearQuestions();
		SpawnQuestions(questions);
	}

	private void ClearQuestions() {
		foreach(Admin_QuestionLineEdit question in QuestionLineList) {
			Destroy(question.gameObject);
		}
		QuestionLineList.Clear();
	}
	private void SpawnQuestions(List<Question> questions) {
		foreach(Question question in questions) {
			Admin_QuestionLineEdit newQuesiton = Instantiate(QuestionLine, QuestionParent, false).GetComponent<Admin_QuestionLineEdit>();
			newQuesiton.SetQuestion(question);
			QuestionLineList.Add(newQuesiton);
			newQuesiton.gameObject.SetActive(true);
		}
	}


	public void UpdateDifficultyDropdown(int[] difficulties) {
		DifficultyDropdown.ClearOptions();
		DifficultyDropdown.options.Add(new Dropdown.OptionData { text = "All" });
		foreach(int i in difficulties) {
			DifficultyDropdown.options.Add(new Dropdown.OptionData {
				text = i.ToString()
			});
		}
		DifficultyDropdown.value = 0;
	}

	public void UpdateCategoryDropdowns(List<Category> categories) {
		CategoryDropdown.ClearOptions();
		CategoryDropdown.options.Add(new Dropdown.OptionData { text = "All" });
		foreach(Category cat in categories) {
			CategoryDropdown.options.Add(new Dropdown.OptionData { text = cat.Name });
		}
		CategoryDropdown.value = 0;
	}

	public void DeleteQuestionLine(Admin_QuestionLineEdit questionLine) {
		if(QuestionLineList.Contains(questionLine)) {
			QuestionLineList.Remove(questionLine);
			Destroy(questionLine.gameObject);
		}
	}





}
