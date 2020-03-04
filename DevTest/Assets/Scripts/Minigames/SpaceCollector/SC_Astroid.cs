using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Astroid : MonoBehaviour {


	[HideInInspector]
	public float RotateSpeed;
	[HideInInspector]
	public float MoveSpeed;

	void Update() {
		transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime), Space.Self);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			SC_ShipController shipController = other.GetComponent<SC_ShipController>();
			shipController.Respawn();
			transform.parent.GetComponent<SC_AstroidController>().DestroyAstroid(this);
		}
	}




}
