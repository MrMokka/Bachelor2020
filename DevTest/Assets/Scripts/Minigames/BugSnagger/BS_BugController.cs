using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_BugController : MonoBehaviour {

	public Transform BugPrefab;
	public Transform BugParent;
	public BS_Controller Controller;

	[Space(5f)]
	public float BugRotateSpeed;

	private List<Bug> BugList = new List<Bug>();
	private AreaController AreaController;

	private float BugMoveDistance;
	private float BugAngle;

	struct Bug {
		public BS_Bug Script;
		public Transform BugTransform;
	}

	void Awake() {
		AreaController = GetComponent<AreaController>();
	}

	void Update() {
		if(BugList.Count > 0)
			MoveBugs();
	}

	private void MoveBugs() {
		/* Move in circle
		for(int i = 0; i < BugList.Count; i++) {
			BugList[i].BugTransform.RotateAround(transform.position, Vector3.forward, BugRotateSpeed * 50 * Time.deltaTime);
			BugList[i].BugTransform.localRotation = Quaternion.identity;
		}
		*/
	}


	public void SpawnBugs(List<string> bugs) {
		BugAngle = 360f / bugs.Count;
		for(int i = 0; i < bugs.Count; i++) {
			Bug bug = new Bug();

			bug.BugTransform = Instantiate(BugPrefab, BugParent, false);
			bug.BugTransform.gameObject.SetActive(true);
			bug.BugTransform.position = transform.position + new Vector3(0, AreaController.SphereRadius, 0);
			bug.BugTransform.RotateAround(transform.position, Vector3.forward, BugAngle * i);
			bug.BugTransform.localRotation = Quaternion.identity;

			bug.Script = bug.BugTransform.GetComponent<BS_Bug>();
			bug.Script.SetText(bugs[i], i+1);

			BugList.Add(bug);
		}
	}

	public void ClearBugs() {
		foreach(Bug bug in BugList) {
			Destroy(bug.BugTransform.gameObject);
		}
		BugList.Clear();
	}

	public void BugClicked(BS_Bug bug) {
		Controller.SetQuestionFilling(bug.GetHiddenAlternativeText());
	}
     
}
