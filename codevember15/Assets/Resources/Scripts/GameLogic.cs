using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {
	
	public AudioClip loop;
	bool loop_is_running = false;
	public GameObject circles_container;
	public GameObject scoring_field;
	public GameObject menu_music;
	public GameObject repeator;
	GameObject temp;
	Queue<GameObject> circles = new Queue<GameObject>();
	int framecount = 0;
	int circlecount = 0;
	int spawn_speed = 70;
	int next_spawn = 70;
	bool is_running = false;

	void Start() {

		if(GameObject.Find("repeator") == null) {
			repeator = new GameObject();
			repeator.name = "repeator";
			DontDestroyOnLoad(repeator);
			repeator.AddComponent<repeatController>();
		}
		if(GameObject.Find("repeator") != null) 
			repeator = GameObject.Find("repeator");

		if(repeator.GetComponent<repeatController>().new_round)
			gameObject.GetComponent<StartGame>().Run(false);
	}

	// play some music
	IEnumerator GameMusicStart() {

		menu_music.GetComponent<AudioSource>().Stop();
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length - 0.45f);
		audio.clip = loop;
		audio.Play();
		// update temp
		loop_is_running = true;
	}
	
	public void setRunning() {

		is_running = true;
		gameObject.AddComponent<Score>();
		circles_container.SetActive(true);
		StartCoroutine(GameMusicStart());
	}
	IEnumerator WaitForMusic() {
		yield return new WaitForSeconds(0.5f);
		menu_music.GetComponent<AudioSource> ().Play ();
	}
	public void unsetRunning() {

		is_running = false;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Stop();
		//wait for Musik Popped
		StartCoroutine (WaitForMusic ());
		//circles_container.SetActive(false);
	}

	public Queue<GameObject> getCircles () {
		return circles;
	}

	// Update is called once per frame
	void Update () {

		if(is_running) {
			framecount++;

			// create a new circle
			if(next_spawn == 0) {
				spawnCircle();
				next_spawn = spawn_speed;
			}

			// increase difficulty
			if(framecount % 60 == 0 && spawn_speed > 30)
				spawn_speed--;

			next_spawn--;

			// check if there is a touchinput hitting
			if (Input.touchCount == 1) {
				//Debug.Log ("touchinput");
				
				//Vector3 pos = Camera.main.ViewportToScreenPoint(Input.GetTouch(0).position);
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
				
				if(hit.collider != null){
					//Debug.Log(hit.transform.gameObject.name);

					// check whether hit object is first one in queue
					if(circles.Dequeue().name.Equals(hit.transform.gameObject.name)) {
						//Debug.Log("right one");
						float score_factor = (CirclesBehaviour.max_lifetime - hit.transform.gameObject.GetComponent<CirclesBehaviour>().lifetime * 1.0f) / CirclesBehaviour.max_lifetime;
					
						// get points
						GameObject.Find("GameManager").GetComponent<Score>().updateScore(
							(int) Mathf.Round(circlecount * score_factor)
						);

						Camera.main.GetComponent<AudioSource>().Play();
						Destroy (hit.transform.gameObject);
						// directly spawn new one
						next_spawn = 0;
					}
					else {
						// game over
						Gameover go = GameObject.Find("GameManager").GetComponent<Gameover>();
						go.PopupGameover();
					}
				}
			}
		}
	}

	public void spawnCircle() {
		// TODO: don't spawn two above each other
		circlecount++;
		GameObject temp = Instantiate(Resources.Load("Prefabs/Circle", typeof(GameObject))) as GameObject;
		temp.transform.SetParent(circles_container.transform);
		temp.name = circlecount.ToString();
		temp.GetComponent<CirclesBehaviour>().setNumber(circlecount);
		circles.Enqueue(temp);
		if (loop_is_running && circlecount % 5 == 0) {
			GameObject.Find("GameManager").GetComponent<AudioSource>().pitch += 0.01f;
		}
	}
}
