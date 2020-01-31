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
		public Answer answer;
	}
	
	public override void LoadQuestion(Question question) {
		QuestionText.text = question.QuestionText;
		LoadTextLines(question.TextLines);
		AlternativeController.ClearAlternatives();
		AlternativeController.CreateAlternative(question.Answers);
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override string GetMinigameType() {
		return Type;
	}

	private void LoadTextLines(List<TextLine> textLines) {
		ClearTextLines();
		foreach(TextLine tl in textLines) {
			GameObject g = Instantiate(QuestionTextTemplate, QuestionParent, false);
			g.SetActive(true);
			Dnd_FillInnText fill = g.GetComponent<Dnd_FillInnText>();
			fill.SetText(tl.text);
			fill.SetInteractable(tl.interactable);
			TextLineAnswer l = new TextLineAnswer {
				textLineObj = g,
				textLine = tl
			};
			if(tl.correctAnswer == null)
				l.answer = null;
			else
				l.answer = tl.correctAnswer.answer;
			textLineAnswerList.Add(l);
		}
	}

	private void ClearTextLines() {
		foreach(TextLineAnswer line in textLineAnswerList) {
			Destroy(line.textLineObj);
		}
		textLineAnswerList.Clear();
	}


}
