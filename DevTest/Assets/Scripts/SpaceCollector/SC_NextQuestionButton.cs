using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_NextQuestionButton : MonoBehaviour {

	public float TriggerTime;
	public Image ProgressDisplay;
	public GameLoader GameLoader;

	private float Timer;
	private bool IsInside;
	private SC_ShipController ShipController;


	void Update() {
		if(IsInside) {
			Timer += Time.deltaTime;
			if(Timer >= TriggerTime) {
				Timer = 0;
				ShipController.Respawn();
				GameLoader.NextQuestion();
			}
		} else {
			if(Timer >= 0) {
				Timer -= Time.deltaTime;
			}
		}
		ProgressDisplay.fillAmount = Timer / TriggerTime;
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player"))
			return;
		IsInside = true;
		if(ShipController == null)
			ShipController = other.GetComponent<SC_ShipController>();
	}

	void OnTriggerExit2D(Collider2D other) {
		if(!other.CompareTag("Player"))
			return;
		IsInside = false;
	}



}
