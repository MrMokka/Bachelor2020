using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_ButtonController : MonoBehaviour {

	public RS_Controller Controller;
	public GameLoader GameLoader;
	public CountdownController CountController;
	public GameObject PausePanel;
	public Option ActivationAction;

	public enum Option {
		ShowInfo,
		PauseGame,
		NextLine,
		NextQuestion
	}

	private GameObject InfoPanel;

	void Awake() {
		InfoPanel = Controller.InfoPanel;
	}

	private void Action() {
		switch(ActivationAction) {
			case Option.ShowInfo:
				CountController.StopTimer();
				InfoPanel.SetActive(true);
				break;
			case Option.PauseGame:
				CountController.StopTimer();
				PausePanel.SetActive(true);
				break;
			case Option.NextLine:
				Controller.SelectNextLine();
				break;
			case Option.NextQuestion:
				GameLoader.NextQuestion();
				break;
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Bullet")) {
			Action();
			Destroy(other.gameObject);
		}
	}


}
