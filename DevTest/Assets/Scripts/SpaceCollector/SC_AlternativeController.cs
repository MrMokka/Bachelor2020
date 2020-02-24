using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AlternativeController : MonoBehaviour {

	public GameObject AlternativeTemplate;
	public Transform AlternativeParent;

	private SC_AreaController AreaController;
	private List<GameObject> Alternatives;

	void Awake() {
		AreaController = GetComponent<SC_AreaController>();
	}



	public void CreateAlternative(List<Alternative> alternatives) {

		foreach(Alternative alternative in alternatives) {
			GameObject obj = Instantiate(AlternativeTemplate, AlternativeParent, false);
			obj.SetActive(true);
			Vector2 spawnPos = new Vector2 {
				x = Random.Range(0, AreaController.Size.x) - AreaController.Size.x / 2,
				y = Random.Range(0, AreaController.Size.y) - AreaController.Size.y / 2
			};
			obj.transform.position += new Vector3(spawnPos.x, spawnPos.y, 0);
			Alternatives.Add(obj);
		}

	}

	public void ClearAlternatives() {
		foreach(GameObject g in Alternatives) {
			Destroy(g);
		}
		Alternatives.Clear();
	}



}
