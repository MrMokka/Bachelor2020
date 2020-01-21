using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCreator : MonoBehaviour {

	public GameObject gridCell;

	private GridLayoutGroup gridGroup;
	private List<List<GameObject>> gridObjList = new List<List<GameObject>>();


	public void Awake() {
		gridGroup = GetComponent<GridLayoutGroup>();
		List<List<int>> gridList;
		gridList = new List<List<int>>{ new List<int>{ 1, 2, 3}, new List<int>{ 4, 5, 6 }, new List<int>{ 7, 8, 9 } };
		CreateGrid(gridList);

	}
	

	public void CreateGrid(List<List<int>> gridList) {
		Vector2 cellSize = new Vector2();
		cellSize.x = 300 / gridList.Count;
		cellSize.y = 300 / gridList[0].Count;
		gridGroup.cellSize = cellSize;
		for(int k = 0; k < gridList.Count; k++) {
			gridObjList.Add(new List<GameObject>());
			foreach(int i in gridList[k]) {
				GameObject obj = Instantiate(gridCell, transform, false);
				obj.GetComponent<Image>().color = new Color(0.1f*i, 0, 0);
				gridObjList[k].Add(obj);
			}
		}
	}


}
