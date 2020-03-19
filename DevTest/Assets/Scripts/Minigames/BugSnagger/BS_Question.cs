using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Question : QuestionScript {

	public void SetHighlight(bool highlighted) {
		if(highlighted) {
			Border.color = HighlightColor;
		} else {
			Border.color = NormalColor;
		}
	}

	public override void SetFilling(string filling) {
		Filling = filling;
		UpdateText();
	}

}
