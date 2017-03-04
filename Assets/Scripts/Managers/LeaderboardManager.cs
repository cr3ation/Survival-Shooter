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

        playerNamesList.text = "";
        scoresList.text = "";
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

    private void ShowScoreOnLeaderboard()
    {
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
