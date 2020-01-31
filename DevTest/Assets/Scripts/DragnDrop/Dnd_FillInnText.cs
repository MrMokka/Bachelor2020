using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_FillInnText : MonoBehaviour {

	public Text textComp;
	public Color normalColor, highlightColor;

	public static GameObject hoveredFillInnText = null;

	private Image outline;
	private bool interactable = true;
	private string text;
	private string filling = "[...]";

	void Awake() {
		outline = GetComponent<Image>();
	}

	void Start() {
		//SetText("Hello {0}, how are you?");
	}

	public void SetInteractable(bool _interactable) {
		interactable = _interactable;
		if(!_interactable)
			outline.enabled = false;
		else
			outline.enabled = true;
	}
	public void SetText(string _text) {
		text = _text;
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
			textComp.text = string.Format(text, filling);
		else
			textComp.text = text;
	}

	public void PointerEnter() {
		if(!interactable)
			return;
		outline.color = highlightColor;
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
