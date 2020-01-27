using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataReader : MonoBehaviour {


	public TextAsset jsonFile;

	void Start() {
		ReadJSON();
	}

	public void ReadJSON() {
		string s = jsonFile.text;
		JSONInfoObject obj = JsonConvert.DeserializeObject<JSONInfoObject>(s);
		
		//print(obj);
	}
	
	private class JSONInfoObject {
		public List<Minigames> Minigames;
	}

	private class Minigames {
		public string Type, Mode;
		public List<string> Category;
		public List<Questions> Questions;
	}

	private class Questions {
		public string Question;
		public List<Answer> Answers;
	}

	private class Answer {
		public List<string> AnswerText;
	}

}
