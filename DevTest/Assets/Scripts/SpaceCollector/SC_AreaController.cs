using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AreaController : MonoBehaviour {


	public bool DrawGizmo;

	[Header("Sphere")]
	public bool DrawSphere;
	public float SphereRadius;

	[Header("Rectangle")]
	public bool DrawRect;
	public Vector3 RectSize;

	void OnDrawGizmosSelected() {
		if(!DrawGizmo)
			return;
		Gizmos.color = Color.green;

		if(DrawSphere) {
			Gizmos.DrawWireSphere(transform.position, SphereRadius);
		}

		if(DrawRect) {
			Gizmos.DrawWireCube(transform.position, RectSize);
		}

		Gizmos.color = Color.white;
	}

}
