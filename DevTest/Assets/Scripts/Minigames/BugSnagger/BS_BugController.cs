using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_BugController : MonoBehaviour {

	public Transform BugPrefab;
	public Transform BugParent;

	private List<Bug> BugList = new List<Bug>();
	private AreaController AreaController;

	struct Bug {
		public BS_Bug Script;
		public Transform BugTransform;
	}

	void Awake() {
		AreaController = GetComponent<AreaController>();
	}


	public void SpawnBugs(List<string> bugs) {
		float angle = 360f / bugs.Count;
		for(int i = 0; i < bugs.Count; i++) {
			Bug bug = new Bug();

			bug.BugTransform = Instantiate(BugPrefab, BugParent, false);
			bug.BugTransform.position = transform.position + new Vector3(0, AreaController.SphereRadius, 0);
			bug.BugTransform.RotateAround(transform.position, Vector3.forward, angle * i);
			bug.BugTransform.localRotation = Quaternion.identity;

			bug.Script = bug.BugTransform.GetComponent<BS_Bug>();
			bug.Script.SetText(bugs[i]);

			BugList.Add(bug);
		}
	}

	public void ClearBugs() {
		foreach(Bug bug in BugList) {
			Destroy(bug.BugTransform.gameObject);
		}
		BugList.Clear();
	}

     
}

/*

	for (int i = 0; i<pieceCount; i++)
     {
         Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
	Vector3 direction = rotation * Vector3.forward;

	Vector3 position = centerPos + (direction * radius);
	Instantiate(prefab, position, rotation);


	if(SpawnTimer >= SpawnInterval) {
			GameObject obj = Instantiate(Astroid, transform, false);
			SC_Astroid astroid = obj.GetComponent<SC_Astroid>();
			astroid.MoveSpeed = Random.Range(AstroidMoveSpeed.min, AstroidMoveSpeed.max);
			astroid.RotateSpeed = Random.Range(AstroidRotateSpeed.min, AstroidRotateSpeed.max);
			obj.transform.position = transform.position + new Vector3(0, AreaController.SphereRadius + Random.Range(-1, 1), 0);
			AstroidList.Add(astroid);
			SpawnTimer = 0;
		}
		SpawnTimer += Time.deltaTime;

*/