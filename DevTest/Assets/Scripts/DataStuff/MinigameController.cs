﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour {

	public abstract int LoadQuestion(Question Question);
	public abstract string GetMinigameMode();
	public abstract int CheckCorrectAnswers();

}
