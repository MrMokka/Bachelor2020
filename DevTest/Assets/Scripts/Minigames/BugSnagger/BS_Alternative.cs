using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Alternative : MonoBehaviour {

	public Text TextObject;

	private int Num = 0;
	private string Text = "";

	public void SetNum(int num) {
		Num = num;
		UpdateText();
	}
	public void SetText(string text) {
		Text = text;
		UpdateText();
	}
	public string GetText() {
		return Text;
	}

	private void UpdateText() {
		TextObject.text = Num + " - " + Text;
	}

}
