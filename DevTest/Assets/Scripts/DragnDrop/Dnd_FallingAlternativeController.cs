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

	private List<GameObject> alternatives = new List<GameObject>();
	private IEnumerator spawnAlterativesRoutine = null;


	public void CreateAlternative(List<Answer> answers) {
		//Can save as object to alow stopping when needed
		if(spawnAlterativesRoutine != null) {
			StopCoroutine(spawnAlterativesRoutine);
		}
		spawnAlterativesRoutine = SpawnAlternatives(answers, 0.5f);
		StartCoroutine(spawnAlterativesRoutine);
	}

	private IEnumerator SpawnAlternatives(List<Answer> answers, float delay) {
		yield return null;
		foreach(Answer a in answers) {
			GameObject g = Instantiate(alternativeTemplate, parent, false);
			g.SetActive(true);
			Dnd_FallingAlternative fall = g.GetComponent<Dnd_FallingAlternative>();
			Dnd_FallingAlternative.Dnd_AltSettings settings = new Dnd_FallingAlternative.Dnd_AltSettings {
				top = top,
				bottom = bottom,
				left = left,
				right = right,
				speed = Random.Range(randomSpeed.min, randomSpeed.max),
				text = a.text,
				startPos = new Vector2(Random.Range(left, right), top),
				fallingParent = parent
			};
			fall.SetValues(settings);
			alternatives.Add(g);
			yield return new WaitForSeconds(delay);
		}
	}

	public void ClearAlternatives() {
		foreach(GameObject g in alternatives) {
			Destroy(g);
		}
		alternatives.Clear();
	}

	[Serializable]
	public struct MinMax {
		public float min;
		public float max;
	}

}
