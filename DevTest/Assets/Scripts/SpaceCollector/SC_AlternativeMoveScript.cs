using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AlternativeMoveScript : MonoBehaviour {

	public float MaxFollowDistance, DragSpeed;
	public Transform Following;

	void FixedUpdate() {
		if(Following == null)
			return;

		//TODO: Need tuning to remove stutter when player only moves a tiny bit
		if(Vector3.Distance(transform.position, Following.position) > MaxFollowDistance) {
			Vector3 direciton = Following.position - transform.position;
			transform.Translate(direciton.normalized * Time.deltaTime * DragSpeed);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(Following != null)
			return;
		other.GetComponent<SC_ShipController>().SetFollower(transform);
		Following = other.transform;
	}

	public void ClearFollowing() {
		Following = null;
	}


}
