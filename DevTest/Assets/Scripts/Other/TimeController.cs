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

	private void SetTime(int timeScale) {
		Time.timeScale = timeScale;
	}


}
