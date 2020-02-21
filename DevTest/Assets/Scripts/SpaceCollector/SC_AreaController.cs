using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AreaController : MonoBehaviour {



	[Header("Gizmo Variables")]
	public bool DrawGizmo;
	public Vector2 Size;

	void OnDrawGizmosSelected() {
		if(!DrawGizmo)
			return;

        Gizmos.color = Color.green;

		Gizmos.DrawWireCube(
			transform.position,
			Size
		);

		Gizmos.color = Color.white;
	}

}
