using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_Controller : MinigameController {

	public string Mode;
	public Transform InfoPanel;
	public Text QuestionText;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject InputFieldTemplate;
	public GameObject TextFieldTemplate;
	public Dnd_FallingAlternativeController AlternativeController;

	private List<TextField> TextFieldList = new List<TextField>();
	private QuestionObject QObject;

	private struct TextField {
		public GameObject questionLineObj;
		public QuestionLine questionLine;
		public Dnd_QuestionInput questionInput;
	}

	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="Question2"></param>
	public override int LoadQuestion(Question Question2) {
		QuestionText.text = Question2.QuestionText;
		QObject = Question2.GetQuestionObject();
		int i = LoadquestionLines(QObject.QuestionLines);
		AlternativeController.ClearAlternatives();
		AlternativeController.CreateAlternative(QObject.Alternatives);
		return i;
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override int CheckCorrectAnswers() {
		int score = 0;
		//Check directly to Dnd_questionInputInnText script
		//Is point of having questionLineAnswer? Hmmm dont think so
		foreach(TextField tf in TextFieldList) {
			if(tf.questionLine.CorrectAlternative == null)
				continue;
			string s1 = tf.questionLine.CorrectAlternative.Text;
			string s2 = tf.questionInput.GetFilling();
			if(s1 == s2) {
				tf.questionInput.outline.color = tf.questionInput.correctColor;
				score++;
			} else
				tf.questionInput.outline.color = tf.questionInput.wrongColor;
			tf.questionInput.ignoreMouse = true;
		}
		return score;
	}

	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="questionLines"></param>
	/// <returns></returns>
	private int LoadquestionLines(List<QuestionLine> questionLines) {
		ClearquestionLines();
		int i = 0;
		foreach(QuestionLine ql in questionLines) {
			GameObject g;
			TextField tf = new TextField();
			if(ql.Text.Contains("{0}")) {
				g = Instantiate(InputFieldTemplate, QuestionParent, false);
				i++;
				Dnd_QuestionInput questionInput = g.GetComponent<Dnd_QuestionInput>();
				questionInput.SetText(ql.Text);
				questionInput.SetInteractable(true);
				tf.questionInput = questionInput;
			} else {
				g = Instantiate(TextFieldTemplate, QuestionParent, false);
				g.GetComponent<Dnd_QuestionText>().SetText(ql.Text);
				ql.CorrectAlternative = null;
			}
			g.SetActive(true);
			
			tf.questionLineObj = g;
			tf.questionLine = ql;

			TextFieldList.Add(tf);

			/*
			GameObject g = Instantiate(InputFieldTemplate, QuestionParent, false);
			g.SetActive(true);
			Dnd_questionInputInnText questionInput = g.GetComponent<Dnd_questionInputInnText>();
			questionInput.SetText(ql.text);
			questionInput.SetInteractable(ql.interactable);
			questionLineAnswer l = new questionLineAnswer {
				questionLineObj = g,
				questionLine = ql,
				questionInput = questionInput
			};
			if(ql.correctAnswer == null) {
				l.answer = null;
			} else {
				//l.answer = ql.correctAnswer.answer;
				i++;
			}*/
		}
		return i;
	}

	private void ClearquestionLines() {
		foreach(TextField line in TextFieldList) {
			Destroy(line.questionLineObj);
		}
		TextFieldList.Clear();
	}


}
