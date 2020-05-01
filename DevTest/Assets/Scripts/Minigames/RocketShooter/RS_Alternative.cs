using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RS_Alternative : MonoBehaviour {

	public Text TextText, NumText;

	private int Num = 0;
	private string Text = "";

	public void SetNum(int num) {
		Num = num;
		NumText.text = num.ToString();
	}
	public void SetText(string text) {
		Text = text;
		TextText.text = text;
	}
	public string GetText() {
		return Text;
	}

	private void UpdateText() {
		TextText.text = Num + " - " + Text;
	}

}
