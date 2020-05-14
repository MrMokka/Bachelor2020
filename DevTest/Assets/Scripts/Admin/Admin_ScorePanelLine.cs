using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin_ScorePanelLine : MonoBehaviour {

	public Text IdText, TotalScoreText;
	public InputField EmailInputField;
	
	[SerializeField]
	private TotalScore TotalScore;

	public void SetInformation(TotalScore totalScore) {
		TotalScore = totalScore;
		IdText.text = TotalScore.Email.Id.ToString(); //Might not be best to use email id here
		TotalScoreText.text = TotalScore.CombinedScore.ToString();
		EmailInputField.text = TotalScore.Email.EmailString;
	}

	public int GetEmailId() {
		return TotalScore.Email.Id;
	}

}
