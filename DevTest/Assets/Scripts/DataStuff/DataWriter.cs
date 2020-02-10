using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataWriter : MonoBehaviour {

	public TextAsset jsonFile;
	public string filePath = "./";
	public string fileName = "Prototype.json";

	private List<QuestionObject> QuestionObjects = new List<QuestionObject>();

	void Start() {


		#region Old way
		/*
		Minigame m = new Minigame {
			Mode = "DnD",
			Type = "Fall",
			Categories = new List<string> {
				"Prototype"
			}
		};

		#region Questoins
		m.AddQuestions("Calculate",
					new List<string> {
						"1 + 1 = {0}",
						"25 * 2 = {0}",
						"x = 5, y = 10",
						"x * 2 + y = {0}",
						"x + y * 1.5 = {0}"
					},
					new List<string> { "11", "2", "32", "46", "50", "62", "79", "80", "94", "20" },
					new List<int> { 1, 4, 9, 9 }
		);
		m.AddQuestions("How many items will this loop return?",
					new List<string> {
						"Answer: {0}",
						"for(int i = 0; i < 10; i++) {",
						"	if(i % 2 == 0) Console.WriteLine(i);",
						"}"
					},
					new List<string> { "3", "4", "5", "6", "7" },
					new List<int> { 2 }
		);
		m.AddQuestions("What is the range of this loop?",
					new List<string> {
						"Answer: {0}",
						"for(int i = 2; i < 200; i++) {"
					},
					new List<string> { "0-200", "1-200", "2-200", "0-199", "1-199", "2-199" },
					new List<int> { 5 }
		);
		m.AddQuestions("Int x = 5, int y = 17, int z = 3",
					new List<string> {
						"Answer: {0}",
						"x * z - y  = ?"
					},
					new List<string> { "70", "2", "-36", "-3", "-2", "-1" },
					new List<int> { 4 }
		);

		#endregion

		string jsonString = JsonUtility.ToJson(m, true);
		File.WriteAllText(filePath + fileName, jsonString);
		*/
		#endregion

		QuestionObject qObj = CreateQuestionObject(
					new List<string> {
						"1 + 1 = {0}",
						"25 * 2 = {0}",
						"x = 5, y = 10",
						"x * 2 + y = {0}",
						"x + y * 1.5 = {0}"
					},
					new List<string> { "11", "2", "32", "46", "50", "62", "79", "80", "94", "20" },
					new List<int> { 1, 4, 9, 9 }
		);

		/*
		Question q = CreateQuestion("Calculate",
					new List<string> {
						"1 + 1 = {0}",
						"25 * 2 = {0}",
						"x = 5, y = 10",
						"x * 2 + y = {0}",
						"x + y * 1.5 = {0}"
					},
					new List<string> { "11", "2", "32", "46", "50", "62", "79", "80", "94", "20" },
					new List<int> { 1, 4, 9, 9 },
					1
		);
		*/

		string jsonString = JsonUtility.ToJson(qObj, true);
		File.WriteAllText(filePath + fileName, jsonString);


		// Write / Read JSON (from wiki)
		//string json = JsonUtility.ToJson(myObject);
		//myObject = JsonUtility.FromJson<MyClass>(json);
		//string jsonString = JsonUtility.ToJson(m, true);
		//File.WriteAllText(filePath + fileName, jsonString);

	}

	private Question CreateQuestion(string QuestionText, List<string> QuestionLines, List<string> alternatives, List<int> correctAlternatives, int weight) {

		Question Question = new Question {
			QuestionText = QuestionText,
			Weight = weight
		};

		QuestionObject QuestionObj = new QuestionObject();

		foreach(string s in alternatives) {
			QuestionObj.Alternatives.Add(new Alternative { Text = s });
		}
		int i = 0;
		foreach(string ql in QuestionLines) {
			if(ql.Contains("{0}")) {
				QuestionObj.QuestionLines.Add(new QuestionLine {
					Text = ql,
					CorrectAlternative = QuestionObj.Alternatives[correctAlternatives[i]]
				});
				i++;
			} else {
				QuestionObj.QuestionLines.Add(new QuestionLine {
					Text = ql,
					CorrectAlternative = null
				});
			}
			Question.QuestionObject = JsonUtility.ToJson(QuestionObj, false);
		}
		return Question;
	}

	private QuestionObject CreateQuestionObject(List<string> QuestionLines, List<string> alternatives, List<int> correctAlternatives) {
		QuestionObject QuestionObj = new QuestionObject();

		foreach(string s in alternatives) {
			QuestionObj.Alternatives.Add(new Alternative { Text = s });
		}
		int i = 0;
		foreach(string ql in QuestionLines) {
			if(ql.Contains("{0}")) {
				QuestionObj.QuestionLines.Add(new QuestionLine {
					Text = ql,
					CorrectAlternative = QuestionObj.Alternatives[correctAlternatives[i]]
				});
				i++;
			} else {
				QuestionObj.QuestionLines.Add(new QuestionLine {
					Text = ql,
					CorrectAlternative = null
				});
			}
		}
		return QuestionObj;
	}



}
