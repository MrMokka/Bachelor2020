using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_AlternativeController : MonoBehaviour {

	public GameObject AlternativeTemplate;
	public Transform AlternativeParent;

	private List<GameObject> Alternatives = new List<GameObject>();


	public List<string> CreateAlternative(List<Alternative> alternatives) {
		int i = 0;
		List<string> targetAlternativeList = new List<string>();
		foreach(Alternative alternative in alternatives) {
			GameObject obj = Instantiate(AlternativeTemplate, AlternativeParent, false);
			obj.SetActive(true);
			RS_Alternative alt = obj.GetComponent<RS_Alternative>();
			alt.SetText(alternative.Text);
			alt.SetNum(i+1);
			Alternatives.Add(obj);
			targetAlternativeList.Add(alternative.Text);
			i++;
		}
		return targetAlternativeList;
	}

	public void ClearAlternatives() {
		foreach(GameObject g in Alternatives) {
			Destroy(g);
		}
		Alternatives.Clear();
	}

}
