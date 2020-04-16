using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_ButtonController : MonoBehaviour {

	public BS_Controller Controller;
	public GameLoader GameLoader;
	public CountdownController CountController;
	public GameObject InfoPanel, PausePanel;
	public bool NextLine, NextQuestion;


	private void Action() {
		if(InfoPanel != null) {
			CountController.StopTimer();
			InfoPanel.SetActive(true);
		} else if(PausePanel != null) {
			CountController.StopTimer();
			PausePanel.SetActive(true);
		} else if(NextLine) {
			Controller.SelectNextLine();
		} else if(NextQuestion) {
			GameLoader.NextQuestion();
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Bullet")) {
			Action();
			Destroy(other.gameObject);
		}
	}


}
