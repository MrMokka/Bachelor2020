using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_QuestionInput : MonoBehaviour {

	public Color normalColor, highlightColor;
	public Image outline;
	public Dnd_QuestionText questionText;

	public static GameObject hoveredFillInnText = null;

	private bool interactable = true;
	private string text;
	private string filling = "[...]";

	void Start() {
		//SetText("Hello {0}, how are you?");
	}

	public void SetInteractable(bool _interactable) {
		interactable = _interactable;
		if(_interactable)
			outline.enabled = true;
		else
			outline.enabled = false;
	}
	public void SetText(string _text) {
		text = _text;
		//Switch this to regex
		if(text.Contains("{0}"))
			SetFilling("[...]");
		else
			UpdateText();
	}

	public void SetFilling(string _filling) {
		filling = _filling;
		UpdateText();
	}

	public string GetFilling() {
		return filling;
	}

	private void UpdateText() {
		if(text.Contains("{0}"))
			questionText.SetText(string.Format(text, filling));
		else
			questionText.SetText(text);
	}

	public void PointerEnter() {
		if(!interactable)
			return;
		outline.color = highlightColor;
		//Set static hover variable to self to indicate mouse is over
		hoveredFillInnText = gameObject;
	}

	public void PointerExit() {
		if(!interactable)
			return;
		outline.color = normalColor;
		if(hoveredFillInnText == gameObject)
			hoveredFillInnText = null;
	}

	

}
