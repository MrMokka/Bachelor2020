using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dnd_FallingAlternativeController : MonoBehaviour {

	public GameObject alternativeTemplate;
	public Transform parent;
	public float fallingSpeed;
	public float top, bottom, left, right;
	public MinMax randomSpeed;

	private List<GameObject> Alternatives = new List<GameObject>();
	private IEnumerator SpawnAlterativesRoutine = null;


	public void CreateAlternative(List<Alternative> alternatives) {
		//Can save as object to alow stopping when needed
		if(SpawnAlterativesRoutine != null) {
			StopCoroutine(SpawnAlterativesRoutine);
		}
		SpawnAlterativesRoutine = SpawnAlternatives(alternatives, 0.5f);
		StartCoroutine(SpawnAlterativesRoutine);
	}

	private IEnumerator SpawnAlternatives(List<Alternative> alternatives, float delay) {
		yield return null;
		foreach(Alternative a in alternatives) {
			GameObject g = Instantiate(alternativeTemplate, parent, false);
			g.SetActive(true);
			Dnd_FallingAlternative fall = g.GetComponent<Dnd_FallingAlternative>();
			Dnd_FallingAlternative.Dnd_AltSettings settings = new Dnd_FallingAlternative.Dnd_AltSettings {
				top = top,
				bottom = bottom,
				left = left,
				right = right,
				speed = Random.Range(randomSpeed.min, randomSpeed.max),
				text = a.Text,
				startPos = new Vector2(Random.Range(left, right), top),
				fallingParent = parent
			};
			fall.SetValues(settings);
			Alternatives.Add(g);
			yield return new WaitForSeconds(delay);
		}
	}

	public void ClearAlternatives() {
		foreach(GameObject g in Alternatives) {
			Destroy(g);
		}
		Alternatives.Clear();
	}

	[Serializable]
	public struct MinMax {
		public float min;
		public float max;
	}

}
