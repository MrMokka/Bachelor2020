using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Question : MonoBehaviour {

	public Color normalColor, correctColor, wrongColor;
	public Text TextObj;
	public Image Border;

	private bool Interactable = true;
	private string Filling = "[...]";
	private string QuestionText;
	private BoxCollider2D Collider;


	void Start() {
		Collider = GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player") || !Interactable)
			return;
		SC_ShipController shipController = other.GetComponent<SC_ShipController>();
		SC_Alternative alternative = shipController.GetFollower();
		if(alternative != null) {
			SetFilling(alternative.GetText());
			shipController.ClearFollower();
		}
	}

	private void UpdateText() {
		TextObj.text = String.Format(QuestionText, Filling);
	}

	public void SetText(string text) {
		QuestionText = text;
		if(!text.Contains("{0}")) {
			Border.enabled = false;
			Interactable = false;
		}
		UpdateText();
	}

	public void SetFilling(string filling) {
		Filling = filling;
		UpdateText();
	}




}
