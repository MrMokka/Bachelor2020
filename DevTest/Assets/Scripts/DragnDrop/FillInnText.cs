using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillInnText : MonoBehaviour {

	public Text textComp;

	private string text;
	private string filling = "[...]";

	public void SetText(string _text) {
		text = _text;
		SetFilling("[...]");
		UpdateText();
	}

	public void SetFilling(string _filling) {
		filling = _filling;
		UpdateText();
	}

	public string GetFilling() {
		return filling;
	}

	public void UpdateTextSize(int size) {
		textComp.fontSize = size;
	}

	private void UpdateText() {
		textComp.text = string.Format(text, filling);
	}

}
