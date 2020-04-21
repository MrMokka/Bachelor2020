using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_NewCategory : MonoBehaviour {

	public InputField Inputfield;
	public Admin_NewQuestion NewQuestionScript;

	private Image Image;
	private bool Fading = false;
	private float TotalTime = 0, Duration = 0.5f;
	private Color CurrentColor, TargetColor = Color.white;

	void Awake() {
		Image = Inputfield.GetComponent<Image>();
	}

	void Update() {

		if(Fading) {
			TotalTime += Time.deltaTime; 
			float lerpTime = TotalTime / Duration;
			Image.color = Color.Lerp(CurrentColor, TargetColor, lerpTime);
			if(lerpTime >= 1)
				Fading = false;
		}

	}

	public void SaveCategory() {
		Category category = new Category {
			Name = Inputfield.text
		};
		bool result = DatabaseConnection.WriteCategoryToDatabase(category);
		StartCoroutine("FadeResult", result);
		Inputfield.text = "";
		NewQuestionScript.UpdateDropdowns();
	}

	private IEnumerator FadeResult(bool result) {
		if(result)
			Image.color = Color.green;
		else
			Image.color = Color.red;
		yield return new WaitForSeconds(1f);
		Fading = true;
		TotalTime = 0;
		CurrentColor = Image.color;
	}

}
