using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemainingScript : MonoBehaviour {

	public Text TimerText;
	public CountdownController TCS;

	private string min, sec;

	void Update() {
		min = TCS.GetMin().ToString();
		sec = TCS.GetSec().ToString();
		TimerText.text = min.PadLeft(2, '0') + ":" + sec.PadLeft(2, '0');
	}

}
