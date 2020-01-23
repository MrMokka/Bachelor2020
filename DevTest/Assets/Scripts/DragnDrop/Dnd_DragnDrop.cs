using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dnd_DragnDrop : MonoBehaviour {


	private GameObject selectedText;

	void Update() {

		if(Input.GetMouseButtonDown(0)) {
			MouseIsOverText();
		}

		if(selectedText != null) {
			if(Input.GetMouseButtonUp(0)) {
				//Release object
			} else {
				selectedText.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if(Input.GetMouseButtonUp(0) && selectedText != null) {

		}

	}


	private void MouseIsOverText() {
		if(EventSystem.current.IsPointerOverGameObject()) {
			GameObject obj = EventSystem.current.currentSelectedGameObject;
			if(obj.CompareTag("AlternativeText")) {
				selectedText = obj;
			}
		}
	}


}
