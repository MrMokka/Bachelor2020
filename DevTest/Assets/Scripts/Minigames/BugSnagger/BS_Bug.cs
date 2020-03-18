using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Bug : MonoBehaviour {

	public Text Text;

	public void SetText(string text) {
		Text.text = text;
	}

	public string GetText() {
		return Text.text;
	}

}
