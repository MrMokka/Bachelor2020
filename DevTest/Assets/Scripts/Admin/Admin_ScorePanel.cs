using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Admin_ScorePanel : MonoBehaviour {

	[Header("Score List")]

	public GameObject ScoreLinePrefab;
	public Transform ScoreLineParent;
	public Text AverageScoreText;
	public GameObject ScoreListPanel;

	private List<TotalScore> TotalScoreList = new List<TotalScore>();
	private List<TotalScore> ScoresFromDatabase = new List<TotalScore>();
	private List<GameObject> ScoreLineList = new List<GameObject>();
	private float AverageScore = 0;

	[Header("View Score")]

	public GameObject ScoreViewLinePrefab;
	public Transform ScoreViewLineParent;
	public GameObject ScoreViewPanel;

	private List<Score> ScoreViewList = new List<Score>();
	private List<GameObject> ScoreViewLineList = new List<GameObject>();



	

	public void OnOpen() {
		ClearScoreLines();
		StartCoroutine("WaitForDatabaseSearch");
	}

	private void ClearScoreLines() {
		foreach(GameObject scoreLine in ScoreLineList) {
			Destroy(scoreLine);
		}
		ScoreLineList.Clear();
	}

	private IEnumerator WaitForDatabaseSearch() {
		ScoreListPanel.SetActive(false);
		for(int i = 0; i < 2; i++) yield return null;

		TotalScoreList.Clear();
		ScoresFromDatabase.Clear();
		ScoresFromDatabase = DatabaseConnection.GetTotalScoreFromDatabase();
		TotalScoreList = ScoresFromDatabase.ToList();
		TotalScoreList.RemoveAll(totalScore => totalScore.Email.EmailString == "" || totalScore.Email.EmailString == null);
		TotalScoreList = TotalScoreList.OrderByDescending(totalScore => totalScore.CombinedScore).ToList();
		CreateScoreLines();
		AverageScoreText.text = AverageScore.ToString();
		ScoreListPanel.SetActive(true);
	}


	private void CreateScoreLines() {
		AverageScore = 0;
		foreach(TotalScore totalScore in TotalScoreList) {
			GameObject totalScoreLine = Instantiate(ScoreLinePrefab, ScoreLineParent, false);
			totalScoreLine.SetActive(true);
			Admin_ScorePanelLine scoreLinePanel = totalScoreLine.GetComponent<Admin_ScorePanelLine>();
			scoreLinePanel.SetInformation(totalScore);
			ScoreLineList.Add(totalScoreLine);
			AverageScore += totalScore.CombinedScore;
		}
		AverageScore /= ScoresFromDatabase.Count;
	}



	public void ViewScoreLine(Admin_ScorePanelLine scorePanelLine) {
		ClearScoreLineView();
		StartCoroutine("WaitForDatabaseSearchView", scorePanelLine.GetEmailId());
	}

	private IEnumerator WaitForDatabaseSearchView(int emailId) {
		ScoreViewPanel.SetActive(false);
		for(int i = 0; i < 2; i++) yield return null;

		ScoreViewList.Clear();
		ScoreViewList = DatabaseConnection.GetScoreFromDatabase(emailId);
		CreateScoreViewLines();
		ScoreViewPanel.SetActive(true);
	}

	private void ClearScoreLineView() {
		foreach(GameObject scoreLine in ScoreViewLineList) {
			Destroy(scoreLine);
		}
		ScoreViewLineList.Clear();
	}

	private void CreateScoreViewLines() {
		foreach(Score score in ScoreViewList) {
			GameObject scoreLine = Instantiate(ScoreViewLinePrefab, ScoreViewLineParent, false);
			scoreLine.SetActive(true);
			Admin_ViewScoreLine viewScoreLine= scoreLine.GetComponent<Admin_ViewScoreLine>();
			viewScoreLine.SetInformation(score);
			ScoreViewLineList.Add(scoreLine);
		}
	}

}
