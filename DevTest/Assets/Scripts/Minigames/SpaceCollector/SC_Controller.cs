using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Controller : MinigameController {

	public string Mode;
	public Transform InfoPanel;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject QuestionTemplate;
	public SC_AlternativeController AlternativeController;


	private QuestionObject QObject;
	private List<QuestionField<SC_Question>> QuestionFieldList = new List<QuestionField<SC_Question>>();


	public override int CheckCorrectAnswers() {
		int score = 0;
		foreach(QuestionField<SC_Question> questionField in QuestionFieldList) {
			if(questionField.Line.CorrectAlternative == null)
				continue;
			string s1 = questionField.Line.CorrectAlternative.Text;
			string s2 = questionField.Script.GetFilling();
			if(s1 == s2) {
				questionField.Script.Border.color = questionField.Script.CorrectColor;
				score++;
			} else
				questionField.Script.Border.color = questionField.Script.WrongColor;
			questionField.Script.Interactable = false;
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
		AlternativeController.CreateAlternative(QObject.Alternatives);
		return i;
	}

	protected override int LoadQuestionLines(List<QuestionLine> questionLines) {
		ClearquestionLines();
		int i = 0;
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
				i++;
				questionField.Script.SetInteractable(true);
			}
			QuestionFieldList.Add(questionField);
		}
		return i;
	}

	private void ClearquestionLines() {
		foreach(QuestionField<SC_Question> questionField in QuestionFieldList) {
			Destroy(questionField.LineObj);
		}
		QuestionFieldList.Clear();
	}


}
