using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour {

	public Color NormalColor, HighlightColor, CorrectColor, WrongColor;
	public Image Border;
	public Text TextObj;
	public bool Interactable;

	protected string Filling = "[...]";
	protected string QuestionText;

	public virtual void SetInteractable(bool interactable) {
		Interactable = interactable;
		if(Interactable)
			Border.enabled = true;
		else
			Border.enabled = false;
	}

	public virtual void SetText(string text) {
		QuestionText = text;
		if(!QuestionText.Contains("{0}")) {
			Border.enabled = false;
			Interactable = false;
		}
		UpdateText();

		TextObj.text = text;
		//Switch this to regex, mby
		if(TextObj.text.Contains("{0}"))
			SetFilling("[...]");
		else
			UpdateText();
	}

	public virtual void SetFilling(string filling) {
		Filling = filling;
		UpdateText();
	}

	public virtual string GetFilling() {
		return Filling;
	}

	protected virtual void UpdateText() {
		TextObj.text = string.Format(QuestionText, Filling);
	}


}
