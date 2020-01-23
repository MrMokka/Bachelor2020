using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_FillInnText : MonoBehaviour {

	public Text textComp;
	public Color normalColor, highlightColor;

	public static GameObject hoveredFillInnText = null;

	private Image outline;
	private string text;
	private string filling = "[...]";

	void Awake() {
		outline = GetComponent<Image>();
	}

	void Start() {
		SetText("Hello {0}, how are you?");
	}

	public void SetText(string _text) {
		text = _text;
		SetFilling("[...]");
	}

	public void SetFilling(string _filling) {
		filling = _filling;
		UpdateText();
	}

	public void SetFillingFromDrag() {
		return;
		print(Dnd_FallingAlternative.gettingDragged);
		if(Dnd_FallingAlternative.gettingDragged == null)
			return;
		SetFilling(Dnd_FallingAlternative.gettingDragged.GetComponent<Dnd_FallingAlternative>().GetAlternativeValue());
	}

	public string GetFilling() {
		return filling;
	}

	private void UpdateText() {
		textComp.text = string.Format(text, filling);
	}

	public void PointerEnter() {
		outline.color = highlightColor;
		hoveredFillInnText = gameObject;
	}

	public void PointerExit() {
		outline.color = normalColor;
		if(hoveredFillInnText == gameObject)
			hoveredFillInnText = null;
	}

	

}
