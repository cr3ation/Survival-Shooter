using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public bool addScores = false;                      // GET or POST & GET
    public GameObject dreamloPrefab;                    // Reference to highscore prefab
    public Text playerNamesList;                        // Reference to list with player names
    public Text scoresList;                             // Reference to list with scores

    private dreamloLeaderBoard leaderBoard;
    private List<dreamloLeaderBoard.Score> highscores;
    private bool stopWorking = false;

	// Use this for initialization
	void Start () {
        // Clear the leadboard
        playerNamesList.text = string.Empty;
        scoresList.text = string.Empty;

        leaderBoard = dreamloPrefab.GetComponent<dreamloLeaderBoard>();

        // Saved name and score
        var playerName = PlayerPrefs.GetString("PlayerName");
        var score = PlayerPrefs.GetInt("SavedScore");
        
        // GET or POST & GET.
        if (addScores && !string.IsNullOrEmpty(playerName))
        {
            leaderBoard.AddScore(playerName, score);
        }
        else
        {
            leaderBoard.LoadScores();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (stopWorking) return;
        if (leaderBoard.ToListHighToLow().Count > 0)
        {
            stopWorking = true;
            highscores = leaderBoard.ToListHighToLow();
            ShowScoreOnLeaderboard();
        }		
	}

    public void AddScore(string playerName, int score)
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            leaderBoard.AddScore(playerName, score);
        }
    }

    public void GetScore()
    {
        leaderBoard.LoadScores();
    }

    private void ShowScoreOnLeaderboard()
    {
        playerNamesList.text = string.Empty;
        scoresList.text = string.Empty;

        var scoresToShow = 8;
        var i = 0;

        foreach (var score in highscores)
        {
            playerNamesList.text += score.playerName.Replace("+", " ") + "\n";
            scoresList.text += score.score + "\n";
            i++;
            if (i == scoresToShow) break;
        }
    }
}
