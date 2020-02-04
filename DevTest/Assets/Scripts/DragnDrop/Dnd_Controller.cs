using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_Controller : MinigameController {

	public string Mode;
	public string Type;
	public Transform InfoPanel;
	public Text QuestionText;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject InputFieldTemplate;
	public GameObject TextFieldTemplate;
	public Dnd_FallingAlternativeController AlternativeController;

	private List<TextField> TextFieldList = new List<TextField>();


	private struct TextField {
		public GameObject textLineObj;
		public TextLine textLine;
		public Dnd_QuestionInput questionInput;
		//public Answer answer;
	}

	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="question"></param>
	public override int LoadQuestion(Question question) {
		QuestionText.text = question.QuestionText;
		int i = LoadTextLines(question.TextLines);
		AlternativeController.ClearAlternatives();
		AlternativeController.CreateAlternative(question.Answers);
		return i;
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override string GetMinigameType() {
		return Type;
	}

	public override int CheckCorrectAnswers() {
		int score = 0;
		//Check directly to Dnd_questionInputInnText script
		//Is point of having TextLineAnswer? Hmmm dont think so
		foreach(TextField tf in TextFieldList) {
			if(tf.textLine.correctAnswer == null)
				continue;
			if(tf.textLine.correctAnswer.answer == null)
				continue;

			//string s = tf.questionInput.GetquestionInputing() + " - " + tf.textLine.correctAnswer.answer.text;
			if(tf.textLine.correctAnswer.answer.text == tf.questionInput.GetFilling()) {
				score++;
				//s += " Correct!";
			}
			//print(s);
		}
		return score;
	}

	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="textLines"></param>
	/// <returns></returns>
	private int LoadTextLines(List<TextLine> textLines) {
		ClearTextLines();
		int i = 0;
		foreach(TextLine tl in textLines) {
			GameObject g;
			TextField tf = new TextField();
			if(tl.text.Contains("{0}")) {
				g = Instantiate(InputFieldTemplate, QuestionParent, false);
				i++;
				Dnd_QuestionInput questionInput = g.GetComponent<Dnd_QuestionInput>();
				questionInput.SetText(tl.text);
				questionInput.SetInteractable(true);
				tf.questionInput = questionInput;
			} else {
				g = Instantiate(TextFieldTemplate, QuestionParent, false);
				g.GetComponent<Dnd_QuestionText>().SetText(tl.text);
			}
			g.SetActive(true);
			
			tf.textLineObj = g;
			tf.textLine = tl;

			TextFieldList.Add(tf);

			/*
			GameObject g = Instantiate(InputFieldTemplate, QuestionParent, false);
			g.SetActive(true);
			Dnd_questionInputInnText questionInput = g.GetComponent<Dnd_questionInputInnText>();
			questionInput.SetText(tl.text);
			questionInput.SetInteractable(tl.interactable);
			TextLineAnswer l = new TextLineAnswer {
				textLineObj = g,
				textLine = tl,
				questionInput = questionInput
			};
			if(tl.correctAnswer == null) {
				l.answer = null;
			} else {
				//l.answer = tl.correctAnswer.answer;
				i++;
			}*/
		}
		return i;
	}

	private void ClearTextLines() {
		foreach(TextField line in TextFieldList) {
			Destroy(line.textLineObj);
		}
		TextFieldList.Clear();
	}


}
