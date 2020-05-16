using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_EditCategoryPanel : MonoBehaviour {

	public Transform CategoryParent;
	public GameObject CategoryLinePrefab, NoCategoriesText, CategoryListPanel;
	public Admin_SaveFeedbackPanel SaveFeedbackPanel;

	private List<Admin_CategoryLineEdit> CategoryLineList = new List<Admin_CategoryLineEdit>();

	public void OnOpen() {
		ClearCategories();
		StartCoroutine("WaitForDatabaseSearch");
	}
	private void ClearCategories() {
		foreach(Admin_CategoryLineEdit cat in CategoryLineList) {
			Destroy(cat.gameObject);
		}
		CategoryLineList.Clear();
	}

	private IEnumerator WaitForDatabaseSearch() {
		CategoryListPanel.SetActive(false);
		for(int i = 0; i < 2; i++) yield return null;
		List<Category> categories = DatabaseConnection.GetCategories();
		SpawnCategories(categories);
	}

	private void SpawnCategories(List<Category> categories) {
		NoCategoriesText.SetActive(false);
		if(categories.Count == 0)
			NoCategoriesText.SetActive(true);
		foreach(Category category in categories) {
			Admin_CategoryLineEdit newCategory = Instantiate(CategoryLinePrefab, CategoryParent, false).GetComponent<Admin_CategoryLineEdit>();
			newCategory.SetCategoryInfo(category);
			CategoryLineList.Add(newCategory);
			newCategory.gameObject.SetActive(true);
		}
		CategoryListPanel.SetActive(true);
	}

	public void UpdateRows() {
		OnOpen();
	}

	public void SaveCategories() {
		List<Category> catList = new List<Category>();
		foreach(Admin_CategoryLineEdit category in CategoryLineList) {
			catList.Add(category.GetCategoryInfo());
		}
		SaveFeedbackPanel.gameObject.SetActive(true);
		SaveFeedbackPanel.ShowText(DatabaseConnection.UpdateCategoriesInDatabase(catList));
	}

}
