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

	// Use this for initialization
	void Start () {
		score = 0;
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

    public bool onGameOver() {
        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        List<ScoreRow> lines = new List<ScoreRow>();
        System.IO.StreamReader file;
        // open the scoring table file
        try
        {
             file = new System.IO.StreamReader(file_name);
        }
        // create table if doesnt exist
        catch (IOException e)
        {
            lines.Add(new ScoreRow(0, score, unixTimestamp.ToString()));
            writeScoreTable(lines);
            return true;
        }

        // load the scoring table from file
        string line;
        int i = 0;
        string[] substrings;
        while ((line = file.ReadLine()) != null) {
            substrings = Regex.Split(line, " ");

            lines.Add(new ScoreRow(i, int.Parse(substrings[1]), substrings[0]));
            i++;
        }

        // check if best score
        bool isBest = false;
        bool isTopFive = false;

        foreach (ScoreRow row in lines) {
            if (row.isBetter(score)) {
                row.updateRow(score, unixTimestamp.ToString());
                isTopFive = true;
                if (row.id == 0) {
                    isBest = true;
                }
                break;
            }
        }

        // update list is necessary
        if (isTopFive) {
            writeScoreTable(lines);
        }

        return isBest;
    }

    private void writeScoreTable(List<ScoreRow> tableRows) {
        System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file_name);
        
        foreach (ScoreRow row in tableRows) {
            fileWriter.WriteLine(row.ToString());
        }
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
