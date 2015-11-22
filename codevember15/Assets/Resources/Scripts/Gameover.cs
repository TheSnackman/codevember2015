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
		GameObject.Find ("GameManager").GetComponent<GameLogic>().unsetRunning();
		
		gameOverBox.SetActive(true);
		endscore.GetComponent<Text>().text = "123";
		//endscore.GetComponent<Text>().text = displayscore.GetComponent<Score>().getScore().ToString();

		Debug.Log ("GameOver!");
	}
}
