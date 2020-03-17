using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {





	public void StopTime() {
		SetTime(0);
	}

	public void StartTime() {
		SetTime(1);
	}

	public void ToggleTime() {
		if(Time.timeScale != 0f)
			Time.timeScale = 1;
		else
			Time.timeScale = 0;
	}

	private void SetTime(int timeScale) {
		Time.timeScale = timeScale;
	}


}
