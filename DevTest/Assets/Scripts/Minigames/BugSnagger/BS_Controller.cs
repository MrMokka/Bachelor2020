using System;
using System.Collections.Generic;
using UnityEngine;

public class BS_Controller : MinigameController {

	public string Mode;
	public Transform InfoPanel;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject QuestionTemplate;
	public BS_AlternativeController AlternativeController;
	public BS_BugController BugController;


	private QuestionObject QObject;
	private List<QuestionField<BS_Question>> QuestionFieldList = new List<QuestionField<BS_Question>>();
	private List<QuestionField<BS_Question>> SelectableQuestionFields = new List<QuestionField<BS_Question>>();
	private QuestionField<BS_Question> SelectedQuestion;
	private int SelectedQuestionCounter = 0;

	public override int CheckCorrectAnswers() {
		int score = 0;
		foreach(QuestionField<BS_Question> questionField in QuestionFieldList) {/*
			if(questionField.Line.CorrectAlternative == null)
				continue;
			string s1 = questionField.Line.CorrectAlternative.Text;
			string s2 = questionField.Script.GetFilling();
			if(s1 == s2) {
				questionField.Script.Border.color = questionField.Script.correctColor;
				score++;
			} else
				questionField.Script.Border.color = questionField.Script.wrongColor;
			questionField.Script.Interactable = false;*/
		}
		return score;
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override int LoadQuestion(Question question) {
		if(question == null) //Temp fix, must be better lol :p
			return 0;
		QObject = question.GetQuestionObject();
		int i = LoadQuestionLines(QObject.QuestionLines);

		//QuestionText.text = question.QuestionText;
		AlternativeController.ClearAlternatives();
		List<string> bugAltList = AlternativeController.CreateAlternative(QObject.Alternatives);
		BugController.SpawnBugs(bugAltList);
		return i;
	}

	protected override int LoadQuestionLines(List<QuestionLine> questionLines) {
		ClearquestionLines();
		int i = 0;
		foreach(QuestionLine questionLine in questionLines) {
			GameObject g = Instantiate(QuestionTemplate, QuestionParent, false);
			g.SetActive(true);
			QuestionField<BS_Question> questionField = new QuestionField<BS_Question> {
				LineObj = g,
				Line = questionLine,
				Script = g.GetComponent<BS_Question>()
			};
			questionField.Script.SetHighlight(false);
			questionField.Script.SetText(questionLine.Text);
			if(questionLine.Text.Contains("{0}")) {
				i++;
				SelectableQuestionFields.Add(questionField);
			}
			QuestionFieldList.Add(questionField);
		}
		if(SelectableQuestionFields.Count > SelectedQuestionCounter) {
			SelectedQuestion = SelectableQuestionFields[SelectedQuestionCounter];
			SelectedQuestion.Script.SetHighlight(true);
		}
		return i;
	}

	private void ClearquestionLines() {
		foreach(QuestionField<BS_Question> questionField in QuestionFieldList) {
			Destroy(questionField.LineObj);
		}
		QuestionFieldList.Clear();
		SelectableQuestionFields.Clear();
		BugController.ClearBugs();
	}

	public void SelectNextLine() {
		SelectedQuestionCounter++;
		if(SelectedQuestionCounter >= SelectableQuestionFields.Count) {
			SelectedQuestionCounter = 0;
		}
		SelectedQuestion.Script.SetHighlight(false);
		SelectedQuestion = SelectableQuestionFields[SelectedQuestionCounter];
		SelectedQuestion.Script.SetHighlight(true);
	}

}
