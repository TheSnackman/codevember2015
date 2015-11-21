using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
        gameObject.SetActive(false);
	}
    /**
    update score with 1 point according to game speed
    **/
    void updateScroe(int speed) {
        double factor = speed / 100;
        int newScore = (int) Math.Round(factor);

        score += newScore;
        GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
    }

    public int getScore() {
        return score;
    }
}
