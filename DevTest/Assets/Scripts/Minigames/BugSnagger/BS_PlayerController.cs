using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_PlayerController : MonoBehaviour {

	public float MoveSpeed, ShootDelay;
	public GameObject Bullet;
	public Transform BulletParent;

	private RectTransform RectTrans, ParentRect;
	private List<GameObject> BulletList = new List<GameObject>();
	private float ShootTimer;

	void Start() {
		RectTrans = GetComponent<RectTransform>();
		ParentRect = transform.parent.GetComponent<RectTransform>();
	}

	void Update() {

		Vector3 pos = RectTrans.anchoredPosition;
		pos.x += Input.GetAxis("Horizontal") * MoveSpeed * 100 * Time.deltaTime;
		pos.x = Mathf.Clamp(pos.x, 0, ParentRect.rect.width - RectTrans.rect.width);
		RectTrans.anchoredPosition = pos;

		if(Input.GetKeyUp(KeyCode.Space)) {
			if(ShootTimer <= 0) {
				Instantiate(Bullet, transform, false).transform.SetParent(BulletParent);
				ShootTimer = ShootDelay;
			}
		}
		if(ShootTimer > 0)
			ShootTimer -= Time.deltaTime;

	}

	public void ClearBullets() {
		foreach(GameObject g in BulletList) {
			Destroy(g);
		}
		BulletList.Clear();
	}



}
