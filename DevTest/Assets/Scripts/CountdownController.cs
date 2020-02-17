using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour {

	public GameLoader GL;

	private CountdownTime Time;
	private float Counter;

	public struct CountdownTime {
		public int Sec, Min;
		public bool Active;
	}

	void Awake() {
		Time = new CountdownTime {
			Active = false
		};
	}
	
	void Update() {
		if(Time.Active)
			CountDown();
	}

	private void CountDown() {
		Counter -= UnityEngine.Time.deltaTime;
		if(Counter <= 0) {
			Time.Sec--;
			if(Time.Sec == -1) {
				Time.Min--;
				Time.Sec = 59;
			}
			Counter = 1;
			if(Time.Min == 0 && Time.Sec == 0) {
				Time.Active = false;
				print("GameOver! Times up!");
				GL.GameOver();
			}
		}
	}

	public void SetTime(int seconds) {
		Time.Sec = seconds % 60;
		Time.Min = Mathf.FloorToInt(seconds / 60);
		if(Time.Min > 59)
			Time.Min = 59;
		Time.Active = true;
	}

	public void StopTimer() {
		Time.Active = false;
	}

	public void ResumeTimer() {
		Time.Active = true;
	}

	public int GetSec() {
		return Time.Sec;
	}

	public int GetMin() {
		return Time.Min;
	}

}

public class CountDown {
	public int Sec, Min;
	public float Counter;

	
}
