using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RS_Target : MonoBehaviour {

	public Text Text;

	private RS_TargetController TargetController;
	private string HiddenAlternativeText;

	void Awake() {
		TargetController = transform.parent.parent.GetComponent<RS_TargetController>();
	}

	public void SetText(string text, int num) {
		Text.text = num.ToString();
		HiddenAlternativeText = text;
	}

	public string GetText() {
		return Text.text;
	}

	public string GetHiddenAlternativeText() {
		return HiddenAlternativeText;
	}

	public void TargetClicked() {
		TargetController.TargetClicked(this);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Bullet")) {
			TargetClicked();
			Destroy(other.gameObject);
		}
	}

}
