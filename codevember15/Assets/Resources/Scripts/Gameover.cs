using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {
	public GameObject gameOverBox;
	public GameObject endscore;
	public GameObject displayscore;

	public void PopupGameover () {

        GameObject.Find("Circles").SetActive(false);
        GameObject go = GameObject.Find("GameManager");
        go.GetComponent<GameLogic>().unsetRunning();

        gameOverBox.SetActive(true);
        endscore.GetComponent<Text>().text = go.GetComponent<Score>().getScore().ToString();

        Debug.Log ("GameOver!");
	}

	public void DestroyPopup() {
		GameObject repeater = GameObject.Find("repeator");
		repeater.GetComponent<repeatController>().new_round = true;

		Application.LoadLevel("superScene");
		//GameObject.Find ("GameManager").GetComponent<GameLogic>().setRunning();
		//gameOverBox.SetActive(false);
	}
}

