using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {
	public GameObject gameOverBox;
	public GameObject endscore;
	public GameObject displayscore;

	IEnumerator WaitForMusic() {

        GameObject.Find("Circles").SetActive(false);
        GameObject gm = GameObject.Find("GameManager");
        gm.GetComponent<GameLogic>().unsetRunning();

		
		GameObject mus = GameObject.Find ("Particles");
		mus.GetComponent<AudioSource>().Play();

        gameOverBox.SetActive(true);
		endscore.GetComponent<Text>().text = gm.GetComponent<Score>().getScore().ToString();
		GameObject replay = GameObject.Find ("GameOverBox");
		replay.SetActive(false);
		yield return new WaitForSeconds(0.8f);
		replay.SetActive(true);

        Debug.Log ("GameOver!");
	}

	public void PopupGameover () {
		
		StartCoroutine(WaitForMusic());
	}

	public void DestroyPopup() {
		GameObject repeater = GameObject.Find("repeator");
		repeater.GetComponent<repeatController>().new_round = true;

		Application.LoadLevel("superScene");
		//GameObject.Find ("GameManager").GetComponent<GameLogic>().setRunning();
		//gameOverBox.SetActive(false);
	}
}

