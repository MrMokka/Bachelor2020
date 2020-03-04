using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SC_AstroidController : MonoBehaviour {
	
	public GameObject Astroid;
	public int MaxAstroidCount;
	public float SpawnInterval;
	public MinMaxFloat AstroidMoveSpeed;
	public MinMaxFloat AstroidRotateSpeed;


	private List<SC_Astroid> AstroidList = new List<SC_Astroid>();
	private float SpawnTimer = 0;
	private SC_AreaController AreaController;

	void Awake() {
		AreaController = GetComponent<SC_AreaController>();
	}

	void Update() {
		SpawnAstroid();
		MoveAstroids();
	}
	
	private void SpawnAstroid() {
		if(AstroidList.Count >= MaxAstroidCount)
			return;
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
	}

	private void MoveAstroids() {
		foreach(SC_Astroid astroid in AstroidList) {
			astroid.transform.RotateAround(transform.position, Vector3.forward, astroid.MoveSpeed * Time.deltaTime);
			astroid.transform.RotateAround(astroid.transform.position, Vector3.forward, astroid.RotateSpeed * Time.deltaTime);
		}
	}

	public void DestroyAstroid(SC_Astroid astroid) {
		AstroidList.Remove(astroid);
		Destroy(astroid.gameObject, 0.1f);
	}

}
