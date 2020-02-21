using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTest : MonoBehaviour {


	public Color normalColor, highlightColor;
	private Image outline;

	void Awake() {
		outline = GetComponent<Image>();
	}

	public void MouseEnter() {
		outline.color = highlightColor;
	}

	public void MouseExit() {
		outline.color = normalColor;
	}

}
