using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject menu;
	public GameObject game_manager;

	// starts the game if active = false
	public void Run(bool active) {

		menu.SetActive(active);
		if(!active) {
			game_manager.GetComponent<GameLogic>().spawnCircle();
			game_manager.GetComponent<GameLogic>().setRunning();
		}
	}
}
