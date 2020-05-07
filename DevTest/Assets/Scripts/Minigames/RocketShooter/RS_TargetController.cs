using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_TargetController : MonoBehaviour {

	public Transform TargetPrefab;
	public Transform TargetParent;
	public RS_Controller Controller;

	[Space(5f)]
	public float TargetSwapDelay;

	private List<Target> TargetList = new List<Target>();
	private AreaController AreaController;

	private float TargetSwapTimer;
	private float TargetAngle;

	struct Target {
		public RS_Target Script;
		public Transform TargetTransform;
	}

	void Awake() {
		AreaController = GetComponent<AreaController>();
	}

	void Update() {
		if(TargetList.Count > 0)
			MoveTargets();

		TargetSwapTimer += Time.deltaTime;
		if(TargetSwapTimer >= TargetSwapDelay) {
			TargetSwapTimer = 0;
			TargetParent.GetChild(Random.Range(0, TargetParent.childCount)).SetSiblingIndex(Random.Range(0, TargetParent.childCount));
		}
	}

	private void MoveTargets() {
		/* Move in circle
		for(int i = 0; i < TargetList.Count; i++) {
			TargetList[i].TargetTransform.RotateAround(transform.position, Vector3.forward, TargetRotateSpeed * 50 * Time.deltaTime);
			TargetList[i].TargetTransform.localRotation = Quaternion.identity;
		}
		*/
	}


	public void SpawnTargets(List<string> targets) {
		TargetAngle = 360f / targets.Count;
		for(int i = 0; i < targets.Count; i++) {
			Target target = new Target();

			target.TargetTransform = Instantiate(TargetPrefab, TargetParent, false);
			target.TargetTransform.gameObject.SetActive(true);
			target.TargetTransform.position = transform.position + new Vector3(0, AreaController.SphereRadius, 0);
			target.TargetTransform.RotateAround(transform.position, Vector3.forward, TargetAngle * i);
			target.TargetTransform.localRotation = Quaternion.identity;

			target.Script = target.TargetTransform.GetComponent<RS_Target>();
			target.Script.SetText(targets[i], i+1);

			TargetList.Add(target);
		}
	}

	public void ClearTargets() {
		foreach(Target target in TargetList) {
			DestroyImmediate(target.TargetTransform.gameObject);
		}
		TargetList.Clear();
	}

	public void TargetClicked(RS_Target target) {
		Controller.SetQuestionFilling(target.GetHiddenAlternativeText());
	}
     
}
