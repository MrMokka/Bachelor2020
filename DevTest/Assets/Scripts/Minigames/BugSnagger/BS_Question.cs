using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Question : QuestionScript {

	public void SetHighlight(bool highlighted) {
		if(highlighted) {
			Border.enabled = true;
			Border.color = HighlightColor;
		} else {
			Border.enabled = false;
		}
	}

	public override void SetFilling(string filling) {
		Filling = filling;
		UpdateText();
	}

}
