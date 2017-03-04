using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public GameObject dreamloPrefab;
    public Text playerNamesList;
    public Text scoresList;

    private bool scoreHasLoaded = false;
    private dreamloLeaderBoard leaderBoard;
    private List<dreamloLeaderBoard.Score> highscores;

	// Use this for initialization
	void Start () {
        leaderBoard = dreamloPrefab.GetComponent<dreamloLeaderBoard>();
        leaderBoard.LoadScores();

        playerNamesList.text = string.Empty;
        scoresList.text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
        if (scoreHasLoaded) return;
        if (leaderBoard.ToListHighToLow().Count > 0)
        {
            scoreHasLoaded = true;
            highscores = leaderBoard.ToListHighToLow();
            ShowScoreOnLeaderboard();
        }		
	}

    public void AddScore(int score)
    {
        var playerName = PlayerPrefs.GetString(MenuManager.playerName);
        leaderBoard.AddScore(playerName, score);
    }

    public void GetScore()
    {
        leaderBoard.LoadScores();
        System.Threading.Thread.Sleep(500);
        ShowScoreOnLeaderboard();
    }

    private void ShowScoreOnLeaderboard()
    {
        playerNamesList.text = string.Empty;
        scoresList.text = string.Empty;

        var scoresToShow = 8;
        var i = 0;

        foreach (var score in highscores)
        {
            playerNamesList.text += score.playerName + "\n";
            scoresList.text += score.score + "\n";
            i++;
            if (i == scoresToShow) break;
        }
    }
}
