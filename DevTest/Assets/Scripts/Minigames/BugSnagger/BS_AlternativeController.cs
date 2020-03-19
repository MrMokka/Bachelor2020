using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_AlternativeController : MonoBehaviour {

	public GameObject AlternativeTemplate;
	public Transform AlternativeParent;

	private List<GameObject> Alternatives = new List<GameObject>();


	public List<string> CreateAlternative(List<Alternative> alternatives) {
		int i = 0;
		List<string> bugAlternativeList = new List<string>();
		foreach(Alternative alternative in alternatives) {
			GameObject obj = Instantiate(AlternativeTemplate, AlternativeParent, false);
			obj.SetActive(true);
			BS_Alternative alt = obj.GetComponent<BS_Alternative>();
			alt.SetText(alternative.Text);
			alt.SetNum(i+1);
			Alternatives.Add(obj);
			bugAlternativeList.Add(alternative.Text);
			i++;
		}
		return bugAlternativeList;
	}

	public void ClearAlternatives() {
		foreach(GameObject g in Alternatives) {
			Destroy(g);
		}
		Alternatives.Clear();
	}

}
