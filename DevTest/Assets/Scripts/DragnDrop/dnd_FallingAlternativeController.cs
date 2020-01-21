using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class dnd_FallingAlternativeController : MonoBehaviour {

	public GameObject textTemplate;
	public Transform parent;
	public float fallingSpeed;
	public float top, bottom;
	public MinMax randomSpawn;
	public MinMax randomSpeed;

	private List<Alternative> alternatives = new List<Alternative>();

	void Start() {
		string[] s = new string[] { "Alt 1", "Alt 2", "Alt 3" };
		CreateAlternative(s);
	}

	void Update() {
		foreach(Alternative alt in alternatives) {
			alt.txt.text = alt.name + " : " + alt.speed;
			alt.obj.Translate(Vector2.down * alt.speed * Time.deltaTime, Space.Self);
			if(alt.obj.localPosition.y < bottom) {
				Vector2 v = alt.obj.localPosition;
				v.y = top;
				alt.obj.localPosition = v;
			}
			alt.speed -= Time.deltaTime;
			if(alt.speed < 35f)
				alt.speed = 35f;

		}
	}

	public void CreateAlternative(string[] texts) {
		StartCoroutine("SpawnAlternatives", texts);
		
	}

	private IEnumerator SpawnAlternatives(string[] texts) {
		yield return null;
		foreach(string s in texts) {
			Alternative alt = new Alternative {
				speed = Random.Range(randomSpeed.min, randomSpeed.max),
				obj = Instantiate(textTemplate, parent, false).transform
			};
			alt.obj.gameObject.SetActive(true);
			Vector2 v = new Vector2(Random.Range(randomSpawn.min, randomSpawn.max), top);
			alt.obj.localPosition = v;
			alt.txt = alt.obj.GetComponent<Text>();
			alt.txt.text = s;
			alt.name = s;
			alternatives.Add(alt);
			yield return new WaitForSeconds(1f);
		}
	}

	public void ClearAlternatives() {
		foreach(Alternative g in alternatives) {
			Destroy(g.obj);
		}
		alternatives.Clear();
	}

	private class Alternative {
		public string name;
		public float speed;
		public Transform obj;
		public Text txt;
	}

	[Serializable]
	public struct MinMax {
		public float min;
		public float max;
	}

}
