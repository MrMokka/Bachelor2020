using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Bug : MonoBehaviour {

	public Text Text;

	private BS_BugController BugController;
	private string HiddenAlternativeText;

	void Awake() {
		BugController = transform.parent.GetComponent<BS_BugController>();
	}

	public void SetText(string text, int num) {
		Text.text = num.ToString();
		HiddenAlternativeText = text;
	}

	public string GetText() {
		return Text.text;
	}

	public string GetHiddenAlternativeText() {
		return HiddenAlternativeText;
	}

	public void BugClicked() {
		BugController.BugClicked(this);
	}

}
