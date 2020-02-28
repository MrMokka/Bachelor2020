using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFunctionTrigger : MonoBehaviour {

	public GameObject TriggerTarget;
	public string FunctionToCall;
	public float Cooldown = 0f;

	private float CooldownTimer = 0f;

	void Update() {
		if(CooldownTimer <= 0)
			return;
		CooldownTimer -= Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player") || FunctionToCall == "")
			return;
		if(CooldownTimer <= 0) {
			TriggerTarget.SendMessage(FunctionToCall);
			CooldownTimer = Cooldown;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(!other.CompareTag("Player") || FunctionToCall == "")
			return;
		if(CooldownTimer <= 0) {
			TriggerTarget.SendMessage(FunctionToCall);
			CooldownTimer = Cooldown;
		}
	}


}
