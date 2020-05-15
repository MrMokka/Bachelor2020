using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_Controller : MinigameController {

	public string Mode;
	public Text QuestionDescription;

	[Space(20f)]
	public Transform QuestionParent;
	public GameObject InputFieldTemplate;
	public GameObject TextFieldTemplate;
	public Dnd_FallingAlternativeController AlternativeController;

	private List<QuestionField<Dnd_QuestionInput>> QuestionFieldList = new List<QuestionField<Dnd_QuestionInput>>();
	private QuestionObject QObject;


	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="Question2"></param>
	public override void LoadQuestion(Question question) {
		Question = question;
		QuestionDescription.text = question.QuestionText;
		QObject = question.GetQuestionObject();
		LoadQuestionLines(QObject.QuestionLines);
		AlternativeController.ClearAlternatives();
		AlternativeController.CreateAlternative(QObject.Alternatives);
	}

	public override string GetMinigameMode() {
		return Mode;
	}

	public override ScoreController.QuestionScore CheckCorrectAnswers() {
		ScoreController.QuestionScore questionScore = new ScoreController.QuestionScore {
			Question = Question,
			Points = 0,
			MaxPoints = TotalAnswers
		};
		//Check directly to Dnd_questionInputInnText script
		//Is point of having questionLineAnswer? Hmmm dont think so
		foreach(QuestionField<Dnd_QuestionInput> questionField in QuestionFieldList) {
			if(questionField.Line.CorrectAlternative == null)
				continue;
			string s1 = questionField.Line.CorrectAlternative.Text;
			string s2 = questionField.Script.GetFilling();
			if(s1 == s2) {
				questionField.Script.outline.color = questionField.Script.correctColor;
				questionScore.Points += 1/TotalAnswers;
			} else
				questionField.Script.outline.color = questionField.Script.wrongColor;
			questionField.Script.ignoreMouse = true;
		}
		return questionScore;
	}

	/// <summary>
	/// Returns the number of possible answers
	/// </summary>
	/// <param name="questionLines"></param>
	/// <returns></returns>
	protected override void LoadQuestionLines(List<QuestionLine> questionLines) {
		ClearquestionLines();
		foreach(QuestionLine questionLine in questionLines) {
			GameObject g;
			QuestionField<Dnd_QuestionInput> questionField = new QuestionField<Dnd_QuestionInput>();
			if(questionLine.Text.Contains("{0}")) {
				g = Instantiate(InputFieldTemplate, QuestionParent, false);
				TotalAnswers++;
				Dnd_QuestionInput questionInput = g.GetComponent<Dnd_QuestionInput>();
				questionInput.SetText(questionLine.Text);
				questionInput.SetInteractable(true);
				questionField.Script = questionInput;
			} else {
				g = Instantiate(TextFieldTemplate, QuestionParent, false);
				g.GetComponent<Dnd_QuestionText>().SetText(questionLine.Text);
				questionLine.CorrectAlternative = null;
			}
			g.SetActive(true);

			questionField.LineObj = g;
			questionField.Line = questionLine;

			QuestionFieldList.Add(questionField);
		}
		//print("Added total: " + TotalAnswers);
	}

	private void ClearquestionLines() {
		foreach(QuestionField<Dnd_QuestionInput> questionField in QuestionFieldList) {
			Destroy(questionField.LineObj);
		}
		QuestionFieldList.Clear();
	}


}
