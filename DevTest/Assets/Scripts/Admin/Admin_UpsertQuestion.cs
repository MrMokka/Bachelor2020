using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Upsert = Update + Insert (new and edit question)
/// </summary>
public class Admin_UpsertQuestion : MonoBehaviour {

	public int[] DifficultyOptions;
	public Admin_EditQuestionPanel EditQuestionPanel;

	public List<Category> Categories { get; private set; }

	void Awake() {
		Categories = new List<Category>();
	}

	public void UpdateCategories() {
		Categories.Clear();
		Categories.Add(new Category { Name = "All" });
		Categories = DatabaseConnection.GetCategories();
	}

	public void SetDifficultyOptions(Dropdown dropdown) {
		dropdown.ClearOptions();
		foreach(int item in DifficultyOptions) {
			dropdown.options.Add(new Dropdown.OptionData {
				text = item.ToString()
			});
		}
	}

	public void UpdateCategoryDropdowns(List<Dropdown> dropdowns) {
		foreach(Dropdown dropdown in dropdowns) {
			dropdown.ClearOptions();
			foreach(Category cat in Categories) {
				dropdown.options.Add(new Dropdown.OptionData { text = cat.Name });
			}
		}
	}

	public void SearchQuestions() {
		string value = EditQuestionPanel.GetSelectedDifficulty();
		List<int> weights = new List<int> { -1 };
		if(value != "All") {
			weights[0] = Int32.Parse(value);
		}
		string selectedCat = EditQuestionPanel.GetSelectedCategory();
		List<Category> category = new List<Category>();
		if(selectedCat == "All") {
			category = Categories;
		} else {
			category.Add(new Category { Name = selectedCat });
		}

		DatabaseConnection.ReadQuestionOptions options = new DatabaseConnection.ReadQuestionOptions {
			CategoryFilter = category,
			WeightFilter = weights,
			RandomOrder = false
		};
		List<Question> questions = DatabaseConnection.ReadQuestionsFromDatabase(options);
		EditQuestionPanel.RespawnQuestions(questions);
	}


}
