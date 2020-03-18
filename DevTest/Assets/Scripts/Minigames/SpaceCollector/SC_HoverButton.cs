using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_HoverButton : MonoBehaviour {


	public float TriggerTime;
	public Image ProgressDisplay;

	[Space(5f)]
	public bool NextQuestion;
	public GameLoader GameLoader;
	[Space(5f)]
	public bool ToggleGameobject;
	public GameObject ToggleableObject;
	[Space(5f)]
	public bool ToggleTime;
	public TimeController TimeController;


	private float Timer;
	private bool IsInside;
	private SC_ShipController ShipController;


	void Update() {
		if(TriggerTime == -1) {
			return;
		}
		if(IsInside) {
			Timer += Time.deltaTime;
			if(Timer >= TriggerTime) {
				Timer = 0;
				ShipController.Respawn();
				if(NextQuestion)
					NextQuesiton();
				if(ToggleGameobject)
					ToggleObject();
				if(ToggleTime)
					ToggleTheTime();
			}
		} else {
			if(Timer >= 0) {
				Timer -= Time.deltaTime;
			}
		}
		ProgressDisplay.fillAmount = Timer / TriggerTime;
	}

	public void NextQuesiton() {
		GameLoader.NextQuestion();
	}

	public void ToggleObject() {
		ToggleableObject.SetActive(!ToggleableObject.activeSelf);
	}

	public void ToggleTheTime() {
		TimeController.ToggleTime();
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
