using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
        GameObject.Find("GameManager").GetComponent<Score>().enabled = false;
	}
    /**
    update score with 1 point according to game speed
    **/
    public void updateScore(int speed) {
        double factor = speed / 100;
        int newScore = (int) Math.Round(factor);

        score += newScore;
        
		// TODO: when UI element score is added, do this
		//GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
    }

    public int getScore() {
        return score;
    }
}
