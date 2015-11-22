using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {
	public GameObject gameOverBox;
	public GameObject endscore;
	public GameObject displayscore;
    public GameObject bestscore;

    IEnumerator WaitForMusic() {

        GameObject.Find("Circles").SetActive(false);
        GameObject gm = GameObject.Find("GameManager");
        gm.GetComponent<GameLogic>().unsetRunning();

		
		GameObject mus = GameObject.Find ("Particles");
		mus.GetComponent<AudioSource>().Play();

        gameOverBox.SetActive(true);
        gm.GetComponent<Score>().onGameOver();

        bestscore.GetComponent<Text>().text = gm.GetComponent<Score>().getBestScore().ToString();
        endscore.GetComponent<Text>().text = gm.GetComponent<Score>().getScore().ToString();
		GameObject GOBox = GameObject.Find ("GameOverBox");
		GOBox.SetActive(false);
		yield return new WaitForSeconds(0.8f);
		GOBox.SetActive(true);

		//gold
		if (gm.GetComponent<Score> ().getScore () >= 5000) {
			GameObject.Find ("bronze").SetActive (false);
			GameObject.Find ("silber").SetActive (false);
			GameObject.Find ("gold").SetActive (true);
		}
		//silber
		else if (gm.GetComponent<Score> ().getScore () >= 2500 && gm.GetComponent<Score> ().getScore () < 5000) {
			GameObject.Find ("bronze").SetActive (false);
			GameObject.Find ("silber").SetActive (true);
			GameObject.Find ("gold").SetActive (false);
		}
		//bronze
		else if (gm.GetComponent<Score> ().getScore () >= 1250 && gm.GetComponent<Score> ().getScore () < 2500) {
			GameObject.Find ("bronze").SetActive (true);
			GameObject.Find ("silber").SetActive (false);
			GameObject.Find ("gold").SetActive (false);
		} else {
			GameObject.Find ("bronze").SetActive (false);
			GameObject.Find ("silber").SetActive (false);
			GameObject.Find ("gold").SetActive (false);
		}

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


