using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject menu;
	public GameObject game_manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	// starts the game if active = false
	public void Run(bool active) {

		menu.SetActive(active);
		if(!active) {
			game_manager.GetComponent<GameLogic>().spawnCircle();
			game_manager.GetComponent<GameLogic>().setRunning();
		}
	}
}
