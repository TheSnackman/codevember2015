using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {
	public GameObject gameOverBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PopupGameover () {

		GameObject.Find("Circles").SetActive(false);
		GameObject.Find ("GameManager").GetComponent<GameLogic>().unsetRunning();

		//TODO: GameObject enable with popup
		gameOverBox.SetActive(true);
		Debug.Log ("GameOver!");
	}
}
