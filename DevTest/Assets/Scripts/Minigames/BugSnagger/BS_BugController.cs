using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_BugController : MonoBehaviour {

	public Transform BugPrefab;
	public Transform BugParent;

	private List<Transform> BugList = new List<Transform>();





	public void SpawnBugs(int count) {
		for(int i = 0; i < count; i++) {
			Transform newBug = Instantiate(BugPrefab, BugParent, false);

			BugList.Add(newBug);
		}
	}



}
