using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject menu;
	public GameObject game_manager;
	GameObject buttonAnimated;
	bool activated;

	void Start() {
		
		buttonAnimated.GetComponent<Animator>().enabled = false;
	}

	// animation
	IEnumerator WaitForAnimation() {

		if(!activated) {
			buttonAnimated.GetComponent<Animator>().enabled = true;
			buttonAnimated.GetComponent<Animator>().SetTime(0.0f);
			buttonAnimated.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
			
			yield return new WaitForSeconds(1);
			game_manager.GetComponent<GameLogic>().setRunning();
		}
		menu.SetActive(activated);
	}

	// starts the game if active = false
	public void Run(bool active) {

		activated = active;
		buttonAnimated = GameObject.Find("StartButton");
		StartCoroutine(WaitForAnimation());
	}
}
