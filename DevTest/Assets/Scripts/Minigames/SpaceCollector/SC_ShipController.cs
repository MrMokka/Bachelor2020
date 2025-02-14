﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShipController : MonoBehaviour {

	public float RotationSpeed, MaxSpeed, AccelerationSpeed, DecelerationSpeed;
	public SC_AlternativeController AlternativeController;
	public Transform RespawnPoint;
	public SC_Alternative Follower;
	

	private Rigidbody2D Rigidbody;
	private float AccelerationTime = 0.5f;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}


	void FixedUpdate() {
		if(Input.GetAxis("Vertical") > 0) {
			AccelerationTime += Time.fixedDeltaTime * AccelerationSpeed;
		} else if(Input.GetAxis("Vertical") < 0) {
			AccelerationTime -= Time.fixedDeltaTime * AccelerationSpeed;
		} else {
			AccelerationTime -= Time.fixedDeltaTime * DecelerationSpeed;
		}
		AccelerationTime = Mathf.Clamp(AccelerationTime, 0, 1);
		Rigidbody.velocity = transform.up * Mathf.Lerp(0, MaxSpeed, AccelerationTime);

		transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal") * RotationSpeed * Time.fixedDeltaTime * -1), Space.Self);

	}

	public void SetFollower(Transform follower) {
		if(follower == null) {
			if(Follower != null)
				Follower.ClearFollowing();
			return;
		}
		if(Follower != null)
			Follower.ClearFollowing();
		Follower = follower.GetComponent<SC_Alternative>();
	}

	public void ClearFollower() {
		if(Follower == null)
			return;
		Follower.ClearFollowing();
		AlternativeController.RespawnAlternative(Follower.gameObject);
		Follower = null;
	}

	public SC_Alternative GetFollower() {
		return Follower;
	}

	public void Respawn() {
		ClearFollower();
		AccelerationTime = 0;
		transform.position = RespawnPoint.position;
	}

}
