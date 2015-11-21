using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject menu;
	public GameObject game_manager;
	public GameObject score;
	GameObject buttonAnimated;
	bool activated;

	void Start() {
		GameObject.Find ("Score").SetActive(false);
		buttonAnimated = GameObject.Find("StartButton");
		buttonAnimated.GetComponent<Animator>().enabled = false;
		GameObject.Find ("GameOverBox").SetActive(false);
	}

	// animation
	IEnumerator WaitForAnimation() {

		if(!activated) {
			buttonAnimated.GetComponent<Animator>().enabled = true;
			buttonAnimated.GetComponent<Animator>().SetTime(0.0f);
			buttonAnimated.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
			
			yield return new WaitForSeconds(1);
			score.SetActive(true);
			game_manager.GetComponent<GameLogic>().setRunning();
		}
		menu.SetActive(activated);
	}

	// starts the game if active = false
	public void Run(bool active) {

		activated = active;
		StartCoroutine(WaitForAnimation());
	}
}
