using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Alternative : MonoBehaviour {

	public float MaxFollowDistance, DragSpeed;
	public Transform Following;
	public Text TextObject;



	void FixedUpdate() {
		if(Following == null)
			return;

		//TODO: Need tuning to remove stutter when player only moves a tiny bit
		if(Vector3.Distance(transform.position, Following.position) > MaxFollowDistance) {
			Vector3 direciton = Following.position - transform.position;
			transform.Translate(direciton.normalized * Time.deltaTime * DragSpeed);
		}

	}

	public void SetText(string text) {
		TextObject.text = text;
	}
	public string GetText() {
		return TextObject.text;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(Following != null || !other.CompareTag("Player"))
			return;
		other.GetComponent<SC_ShipController>().SetFollower(transform);
		Following = other.transform;
	}

	public void ClearFollowing() {
		Following = null;
	}

}
