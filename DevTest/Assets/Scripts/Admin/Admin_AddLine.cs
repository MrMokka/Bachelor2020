using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_AddLine : MonoBehaviour {

	public GameObject Line;
	public Transform SettingsButtonHolder;
	public List<GameObject> LineList = new List<GameObject>();
	

	public void AddLine() {
		GameObject line = Instantiate(Line, transform, false);
		LineList.Add(line);
		SettingsButtonHolder.SetAsLastSibling();
	}

	public void RemoveLine() {
		if(LineList.Count > 0) {
			GameObject toDelete = LineList[LineList.Count - 1];
			LineList.Remove(toDelete);
			DestroyImmediate(toDelete);
		}
	}

	public List<GameObject> GetLineList() {
		return LineList;
	}

}
