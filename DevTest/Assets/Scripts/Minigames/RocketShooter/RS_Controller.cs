using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RS_Controller : MinigameController {

	public string Mode;
	public Text QuestionDescription;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject QuestionTemplate;
	public RS_AlternativeController AlternativeController;
	public RS_TargetController TargetController;
	public RS_PlayerController PlayerController;


	private QuestionObject QObject;
	private List<QuestionField<RS_Question>> QuestionFieldList = new List<QuestionField<RS_Question>>();
	private List<QuestionField<RS_Question>> SelectableQuestionFields = new List<QuestionField<RS_Question>>();
	private QuestionField<RS_Question> SelectedQuestion;
	private int SelectedQuestionCounter = 0;

	public override ScoreController.QuestionScore CheckCorrectAnswers() {
		ScoreController.QuestionScore questionScore = new ScoreController.QuestionScore {
			Question = Question,
			Points = 0
		};
		foreach(QuestionField<RS_Question> questionField in QuestionFieldList) {
			if(questionField.Line.CorrectAlternative == null)
				continue;
			string s1 = questionField.Line.CorrectAlternative.Text;
			string s2 = questionField.Script.GetFilling();
			if(s1 == s2) {
				questionField.Script.Border.color = questionField.Script.CorrectColor;
				questionScore.Points++;
			} else
				questionField.Script.Border.color = questionField.Script.WrongColor;
			//questionField.Script.Interactable = false;
		}
		questionScore.MaxPoints = TotalAnswers;
		return questionScore;
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override void LoadQuestion(Question question) {
		Question = question;
		if(question == null) //Temp fix, must be better lol :p
			return;
		QuestionDescription.text = question.QuestionText;
		QObject = question.GetQuestionObject();
		LoadQuestionLines(QObject.QuestionLines);

		//QuestionText.text = question.QuestionText;
		AlternativeController.ClearAlternatives();
		List<string> targetAltList = AlternativeController.CreateAlternative(QObject.Alternatives);
		TargetController.SpawnTargets(targetAltList);
	}

	protected override void LoadQuestionLines(List<QuestionLine> questionLines) {
		ClearQuestionLines();
		foreach(QuestionLine questionLine in questionLines) {
			GameObject g = Instantiate(QuestionTemplate, QuestionParent, false);
			g.SetActive(true);
			QuestionField<RS_Question> questionField = new QuestionField<RS_Question> {
				LineObj = g,
				Line = questionLine,
				Script = g.GetComponent<RS_Question>()
			};
			questionField.Script.SetHighlight(false);
			questionField.Script.SetText(questionLine.Text);
			if(questionLine.Text.Contains("{0}")) {
				TotalAnswers++;
				SelectableQuestionFields.Add(questionField);
			}
			QuestionFieldList.Add(questionField);
		}
		SelectedQuestionCounter = 0;
		SelectedQuestion = SelectableQuestionFields[0];
		SelectedQuestion.Script.SetHighlight(true);
		//print("Added total: " + TotalAnswers);
	}

	private void ClearQuestionLines() {
		foreach(QuestionField<RS_Question> questionField in QuestionFieldList) {
			Destroy(questionField.LineObj);
		}
		QuestionFieldList.Clear();
		SelectableQuestionFields.Clear();
		TargetController.ClearTargets();
		PlayerController.ClearBullets();
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

	public void SetQuestionFilling(string filling) {
		SelectedQuestion.Script.SetFilling(filling);
	}

}
