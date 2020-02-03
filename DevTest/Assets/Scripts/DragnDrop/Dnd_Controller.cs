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
	public GameObject QuestionTextTemplate;
	public Dnd_FallingAlternativeController AlternativeController;

	private List<TextLineAnswer> textLineAnswerList = new List<TextLineAnswer>();


	private struct TextLineAnswer {
		public GameObject textLineObj;
		public TextLine textLine;
		public Dnd_FillInnText fill;
		public Answer answer;
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
		string s = "";
		//Check directly to Dnd_FillInnText script
		//Is point of having TextLineAnswer?
		foreach(TextLineAnswer tla in textLineAnswerList) {
			if(tla.textLine.correctAnswer != null) {
				if(tla.textLine.correctAnswer.answer != null) {
					s = tla.fill.GetFilling() + " - " + tla.textLine.correctAnswer.answer.text;
					if(tla.textLine.correctAnswer.answer.text == tla.fill.GetFilling()) {
						score++;
						s += " Score!";
					}
					print(s);
				}
			}
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
			GameObject g = Instantiate(QuestionTextTemplate, QuestionParent, false);
			g.SetActive(true);
			Dnd_FillInnText fill = g.GetComponent<Dnd_FillInnText>();
			fill.SetText(tl.text);
			fill.SetInteractable(tl.interactable);
			TextLineAnswer l = new TextLineAnswer {
				textLineObj = g,
				textLine = tl,
				fill = fill
			};
			if(tl.correctAnswer == null) {
				l.answer = null;
			} else {
				//l.answer = tl.correctAnswer.answer;
				i++;
			}
			textLineAnswerList.Add(l);
		}
		return i;
	}

	private void ClearTextLines() {
		foreach(TextLineAnswer line in textLineAnswerList) {
			Destroy(line.textLineObj);
		}
		textLineAnswerList.Clear();
	}


}
