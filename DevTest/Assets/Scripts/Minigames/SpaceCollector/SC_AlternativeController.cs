using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AlternativeController : MonoBehaviour {

	public GameObject AlternativeTemplate;
	public Transform AlternativeParent;

	private SC_AreaController AreaController;
	private List<GameObject> Alternatives = new List<GameObject>();

	void Awake() {
		AreaController = GetComponent<SC_AreaController>();
	}



	public void CreateAlternative(List<Alternative> alternatives) {

		foreach(Alternative alternative in alternatives) {
			GameObject obj = Instantiate(AlternativeTemplate, AlternativeParent, false);
			obj.SetActive(true);
			Vector2 spawnPos = new Vector2 {
				x = Random.Range(0, AreaController.RectSize.x) - AreaController.RectSize.x / 2,
				y = Random.Range(0, AreaController.RectSize.y) - AreaController.RectSize.y / 2
			};
			obj.transform.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
			obj.GetComponent<SC_Alternative>().SetText(alternative.Text);
			Alternatives.Add(obj);
		}

	}

	public void RespawnAlternative(GameObject alternative) {
		Vector2 spawnPos = new Vector2 {
			x = Random.Range(0, AreaController.RectSize.x) - AreaController.RectSize.x / 2,
			y = Random.Range(0, AreaController.RectSize.y) - AreaController.RectSize.y / 2
		};
		alternative.transform.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
	}

	public void RespawnAlternative(Transform alternative) {
		alternative.transform.localPosition = Vector3.zero;
		Vector2 spawnPos = new Vector2 {
			x = Random.Range(0, AreaController.RectSize.x) - AreaController.RectSize.x / 2,
			y = Random.Range(0, AreaController.RectSize.y) - AreaController.RectSize.y / 2
		};
		alternative.transform.localPosition += new Vector3(spawnPos.x, spawnPos.y, 0);
	}

	public void ClearAlternatives() {
		foreach(GameObject g in Alternatives) {
			Destroy(g);
		}
		Alternatives.Clear();
	}



}
