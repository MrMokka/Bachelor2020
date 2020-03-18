using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnswerPanel : MonoBehaviour {

	public GameObject alternative;
	public Transform answerHolder;

	private List<FallingAlternative> alternativesList = new List<FallingAlternative>();
	private int selectedAlternative = -1;

	public void CreateAlternatives(string[] alternatives) {
		foreach(string s in alternatives) {
			GameObject gm = Instantiate(alternative, answerHolder, true);
			FallingAlternative fa = gm.GetComponent<FallingAlternative>();
			fa.alternativeText.text = s;
			alternativesList.Add(fa);
		}
		selectedAlternative = -1;
	}

	public void HighlightAlternative(int i) {
		if(selectedAlternative != -1) {
			alternativesList[selectedAlternative].highlighter.enabled = false;
		}
		selectedAlternative = i;
		alternativesList[selectedAlternative].highlighter.enabled = true;
	}

	public FallingAlternative SelectedAlternative() {
		return alternativesList[selectedAlternative];
	}


}
