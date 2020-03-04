using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFitter2D : MonoBehaviour {

	public float Delay = 0.1f;

	void Start() {
		Invoke("SetColliderSize", Delay);
	}
	private void SetColliderSize() {
		GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
	}

}
