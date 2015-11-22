using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Score : MonoBehaviour {

    private int score;
    private string file_name = "scoreTable.dat";
    private int bestScore;

	// Use this for initialization
	void Start () {
		score = 0;
        bestScore = 0;
		GameObject.Find("Score").GetComponent<Text>().text = "Score: " + score.ToString();
	}

    /**
    update score with 1 point according to game speed
    **/
    public void updateScore(int toAdd) {
        score += toAdd;
        
		// TODO: when UI element score is added, do this
		GameObject.Find("Score").GetComponent<Text>().text = "Score: " + score.ToString();
    }

    public void onGameOver() {
        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        List<ScoreRow> highscoreTable = new List<ScoreRow>();
        System.IO.StreamReader file;
        // open the scoring table file
        try
        {
             file = new System.IO.StreamReader(file_name);
        }
        // create table if doesnt exist
        catch (IOException e)
        {
            Debug.Log("Writing a new highscore table");
            highscoreTable.Add(new ScoreRow(0, score, unixTimestamp.ToString()));
            writeScoreTable(highscoreTable);
            return;
        }

        // load the scoring table from file
        string[] substrings;

        string fileStr = file.ReadToEnd();
        string[] txtlines = fileStr.Split(new char[] { '\n' });
        file.Close();

        for (int i = 0; i < txtlines.Length && highscoreTable.Count < 5; i++) {
            if (txtlines[i] == "") {
                continue;
            }

            substrings = Regex.Split(txtlines[i], " ");
            ScoreRow row = new ScoreRow(i, int.Parse(substrings[1]), substrings[0]);

            if (i==0)
            {
                bestScore = int.Parse(substrings[1]);
            }

            if (row.isBetter(score))
            {
                if (highscoreTable.Count < 4)
                {
                    if (i == 0)
                    {
                        bestScore = score;
                    }
                    highscoreTable.Add(new ScoreRow(i, score, unixTimestamp.ToString()));
                    row.id++;
                    highscoreTable.Add(row);
                } else {
                    row.updateRow(score, unixTimestamp.ToString());
                    highscoreTable.Add(row);
                }
            } else {
                highscoreTable.Add(row);
            }
        }

        if (highscoreTable.Count < 5) {
            highscoreTable.Add(new ScoreRow(highscoreTable.Count, score, unixTimestamp.ToString()));
        }

        // update list is necessary
        writeScoreTable(highscoreTable);
    }

    public int getBestScore() {
        return bestScore;
    }

    private void writeScoreTable(List<ScoreRow> tableRows) {
        System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file_name);
        
        foreach (ScoreRow row in tableRows) {
            fileWriter.WriteLine(row.ToString());
        }

        fileWriter.Close();
    }

    public int getScore() {
        return score;
    }

    class ScoreRow {
        public int id;
        int score;
        string timestamp;

        public ScoreRow(int id, int score, string timestamp) {
            this.id = id;
            this.score = score;
            this.timestamp = timestamp;
        }

        public void updateRow(int score, string timestamp) {
            this.score = score;
            this.timestamp = timestamp;
        }

        public bool isBetter(int otherScore) {
            return otherScore > this.score;
        }

        public string ToString() {
            return timestamp + " " + score;
        }
    }
}
