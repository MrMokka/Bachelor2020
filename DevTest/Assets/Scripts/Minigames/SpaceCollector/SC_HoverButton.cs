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
	public bool ToggleTime;
	public GameObject ToggleableObject;
	public CountdownController CountdownController;


	private float Timer;
	private bool IsInside;
	private SC_ShipController ShipController;
	private float ButtonActivationDelay = 5f;
	private float ActivationTimer = 0f;


	void Update() {
		if(ActivationTimer >= 0) {
			ActivationTimer -= UnityEngine.Time.deltaTime;
			return;
		}
		if(IsInside) {
			Timer += Time.deltaTime;
			if(Timer >= TriggerTime) {
				Timer = 0;
				ActivationTimer = ButtonActivationDelay;
				if(NextQuestion) {
					NextQuesiton();
					ShipController.Respawn();
				}
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
		CountdownController.ToggleTimer();
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
