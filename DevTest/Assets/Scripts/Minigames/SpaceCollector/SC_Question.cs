using UnityEngine;

public class SC_Question : QuestionScript {

	void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player") || !Interactable)
			return;
		SC_ShipController shipController = other.GetComponent<SC_ShipController>();
		SC_Alternative alternative = shipController.GetFollower();
		if(alternative != null) {
			SetFilling(alternative.GetText());
			shipController.ClearFollower();
		}
	}


}
