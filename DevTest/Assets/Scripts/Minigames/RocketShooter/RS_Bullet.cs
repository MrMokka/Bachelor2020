using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_Bullet : MonoBehaviour {

	public float MoveSpeed = 2f;


	void Update() {
		transform.Translate(0, MoveSpeed * 100 * Time.deltaTime, 0);
	}

}
