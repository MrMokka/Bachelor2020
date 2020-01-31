using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour {

	public abstract void LoadQuestion(Question question);
	public abstract string GetMinigameType();
	public abstract string GetMinigameMode();

}
