using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_CategoryLineEdit : MonoBehaviour {

	public Toggle IsActiveToggle;
	public Text Id, Name;

	public void SetCategoryInfo(Category category) {
		Id.text = category.Id.ToString();
		Name.text = category.Name;
		IsActiveToggle.isOn = category.Active;
	}

	public Category GetCategoryInfo() {
		Category cat = new Category {
			Id = Convert.ToInt32(Id.text),
			Name = Name.text,
			Active = IsActiveToggle.isOn
		};
		return cat;
	}

}
