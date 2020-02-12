using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_QuestionText : MonoBehaviour {

	public Text QuestionTextComponent;

	public void SetText(string text) {
		QuestionTextComponent.text = text;
	}

}
