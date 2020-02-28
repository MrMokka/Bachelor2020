using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFitter2D : MonoBehaviour {

	void Start() {
		Invoke("SetColliderSize", 0.1f);
	}
	private void SetColliderSize() {
		GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
	}

}
