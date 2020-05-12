using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Controller : MinigameController {

	public string Mode;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject QuestionTemplate;
	public SC_AlternativeController AlternativeController;


	private QuestionObject QObject;
	private List<QuestionField<SC_Question>> QuestionFieldList = new List<QuestionField<SC_Question>>();


	public override ScoreController.QuestionScore CheckCorrectAnswers() {
		ScoreController.QuestionScore questionScore = new ScoreController.QuestionScore {
			Question = Question,
			Points = 0
		};
		foreach(QuestionField<SC_Question> questionField in QuestionFieldList) {
			if(questionField.Line.CorrectAlternative == null)
				continue;
			string s1 = questionField.Line.CorrectAlternative.Text;
			string s2 = questionField.Script.GetFilling();
			if(s1 == s2) {
				questionField.Script.Border.color = questionField.Script.CorrectColor;
				questionScore.Points++;
			} else
				questionField.Script.Border.color = questionField.Script.WrongColor;
			questionField.Script.Interactable = false;
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
		QObject = question.GetQuestionObject();
		LoadQuestionLines(QObject.QuestionLines);

		//QuestionText.text = question.QuestionText;
		AlternativeController.ClearAlternatives();
		AlternativeController.CreateAlternative(QObject.Alternatives);
	}

	protected override void LoadQuestionLines(List<QuestionLine> questionLines) {
		ClearquestionLines();
		foreach(QuestionLine questionLine in questionLines) {
			GameObject g = Instantiate(QuestionTemplate, QuestionParent, false);
			g.SetActive(true);
			QuestionField<SC_Question> questionField = new QuestionField<SC_Question> { 
				LineObj = g,
				Line = questionLine,
				Script = g.GetComponent<SC_Question>()
			};
			questionField.Script.SetText(questionLine.Text);
			if(questionLine.Text.Contains("{0}")) {
				TotalAnswers++;
				questionField.Script.SetInteractable(true);
			}
			QuestionFieldList.Add(questionField);
		}
		//print("Added total: " + TotalAnswers);
	}

	private void ClearquestionLines() {
		foreach(QuestionField<SC_Question> questionField in QuestionFieldList) {
			Destroy(questionField.LineObj);
		}
		QuestionFieldList.Clear();
	}


}
