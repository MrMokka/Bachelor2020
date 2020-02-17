using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_PanelController : MonoBehaviour {


	public GameObject ActivePanel;

	public void SetActivePanel(GameObject newPanel) {
		if(ActivePanel == null) {
			Debug.LogError("No active panel!");
			return;
		}
		ActivePanel.SetActive(false);
		ActivePanel = newPanel;
		ActivePanel.SetActive(true);
	}


}
