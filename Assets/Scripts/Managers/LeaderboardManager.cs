using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public struct Score
    {
        public string playerName;
        public int score;
        public int seconds;
        public string shortText;
        public string dateString;
    }

    public bool addScores = false;                      // GET or POST & GET
    public Text playerNamesList;                        // Reference to list with player names
    public Text scoresList;                             // Reference to list with scores

    private List<Score> highscores;
    private bool stopWorking = false;

    string dreamloWebserviceURL = "http://dreamlo.com/lb/";
    string privateKey = "p3J2getC6USe5LnsuQPGkQP4joL4S_zk23ublv8DQQSQ";
    string publicKey = "58b9ee3768fc0c0c4c878d54";

    string highScores = "";

    // Use this for initialization
    void Start () {
        // Clear the leadboard. No need if only adding score
        if (!addScores)
        {
            playerNamesList.text = string.Empty;
            scoresList.text = string.Empty;
        }

        // Saved name and score
        var playerName = PlayerPrefs.GetString("PlayerName");
        var score = PlayerPrefs.GetInt("SavedScore");

        // Clean player names
        playerName = Clean(playerName);
        
        // GET or POST & GET.
        if (addScores && !string.IsNullOrEmpty(playerName))
        {
            StartCoroutine(IAddScore(playerName, score));
        }
        else
        {
            StartCoroutine(IGetScore());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (addScores == true || stopWorking == true) return;
        if (ToListHighToLow().Count > 0)
        {
            stopWorking = true;
            highscores = ToListHighToLow();
            ShowScoreOnLeaderboard();
        }		
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

    private void AddScore(string playerName, int score)
    {
        StartCoroutine(IAddScore(playerName, score));            
    }

    public void LoadScore()
    {
        highscores.Clear();
        highScores = "";    
        StartCoroutine(IGetScore());
    }

    #region Talk to leaderboard
    IEnumerator IAddScore(string playerName, int score)
    {
        var url = dreamloWebserviceURL + privateKey + "/add/" + WWW.EscapeURL(playerName) + "/" + score.ToString();
        WWW www = new WWW(url);
        yield return www;
    }

    IEnumerator IGetScore()
    {
        var url = dreamloWebserviceURL + publicKey + "/pipe";
        WWW www = new WWW(url);
        yield return www;
        highScores = www.text;
    }
    #endregion

    #region Helper functions
    string[] ToStringArray()
    {
        if (this.highScores == null) return null;
        if (this.highScores == "") return null;

        string[] rows = this.highScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

    List<Score> ToListLowToHigh()
    {
        Score[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<Score>();

        List<Score> genericList = new List<Score>(scoreList);

        genericList.Sort((x, y) => x.score.CompareTo(y.score));

        return genericList;
    }

    List<Score> ToListHighToLow()
    {
        Score[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<Score>();

        List<Score> genericList = new List<Score>(scoreList);

        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }

    Score[] ToScoreArray()
    {
        string[] rows = ToStringArray();
        if (rows == null) return null;

        int rowcount = rows.Length;

        if (rowcount <= 0) return null;

        Score[] scoreList = new Score[rowcount];

        for (int i = 0; i < rowcount; i++)
        {
            string[] values = rows[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

            Score current = new Score();
            current.playerName = values[0];
            current.score = 0;
            current.seconds = 0;
            current.shortText = "";
            current.dateString = "";
            if (values.Length > 1) current.score = CheckInt(values[1]);
            if (values.Length > 2) current.seconds = CheckInt(values[2]);
            if (values.Length > 3) current.shortText = values[3];
            if (values.Length > 4) current.dateString = values[4];
            scoreList[i] = current;
        }

        return scoreList;
    }

    // Keep pipe and slash out of names
    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        return s;

    }

    int CheckInt(string s)
    {
        int x = 0;

        int.TryParse(s, out x);
        return x;
    }
    #endregion











}
