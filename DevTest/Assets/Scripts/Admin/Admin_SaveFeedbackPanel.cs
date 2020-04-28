using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_SaveFeedbackPanel : MonoBehaviour {

	public Text SavedText, FailedText;

	public void BackClick() {
		SavedText.gameObject.SetActive(false);
		FailedText.gameObject.SetActive(false);
	}

	public void ShowText(bool quesitonWasSaved) {
		if(quesitonWasSaved)
			SavedText.gameObject.SetActive(true);
		else
			FailedText.gameObject.SetActive(true);
	}

}
