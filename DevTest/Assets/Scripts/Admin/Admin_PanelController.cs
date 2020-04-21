using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_PanelController : MonoBehaviour {


	public GameObject ActivePanel;
	public GameObject LastActivePanel;

	void Start() {
		if(ActivePanel != null)	
			ActivePanel.SetActive(true);
	}

	public void SetActivePanel(GameObject newPanel) {
		if(ActivePanel != null) {
			LastActivePanel = ActivePanel;
			ActivePanel.SetActive(false);
		}
		ActivePanel = newPanel;
		ActivePanel.SetActive(true);
	}

	public void DisableActivePanel() {
		if(LastActivePanel == null) {
			Debug.Log("No last active panel exist.");
			return;
		}
		ActivePanel.SetActive(false);
		LastActivePanel.SetActive(true);
		ActivePanel = LastActivePanel;
		LastActivePanel = null;
	}

	public void LogMessage(string msg) {
		Debug.Log(msg);
	}

}
