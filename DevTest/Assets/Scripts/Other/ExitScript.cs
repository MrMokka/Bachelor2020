using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

	public void QuitAppplication() {
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
