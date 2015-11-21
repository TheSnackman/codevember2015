using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PopupGameover () {
		GameObject.Find("Circles").SetActive(false);
		//TODO: GameObject enalbe with popup
		Debug.Log ("GameOver!");
	}
}
