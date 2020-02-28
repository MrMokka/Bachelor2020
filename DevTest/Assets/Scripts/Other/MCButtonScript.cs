using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCButtonScript : MonoBehaviour {

	public ColorBlock colors;
	public Color imageNormalColor;

	private Text text;
	private Button button;
	private Image image;

	void Awake() {
		text = transform.GetChild(0).GetComponent<Text>();
		button = GetComponent<Button>();
		image = GetComponent<Image>();
	}


	public void SetColor(Color color) {
		image.color = color;
	}

	public void SetText(string answer) {
		text.text = answer;
	}

	public void SetActive(bool active) {
		button.interactable = active;
		text.text = "";
	}

	public string GetText() {
		return text.text;
	}

	public void ResetButton() {
		button.colors = colors;
		image.color = imageNormalColor;
		button.interactable = true;
	}


}
