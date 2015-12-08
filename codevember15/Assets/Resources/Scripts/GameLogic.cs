using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {
	
	public AudioClip loop;
	bool loop_is_running = false;
	bool gameover = false;
	public GameObject circles_container;
	public GameObject scoring_field;
	public GameObject menu_music;
	GameObject repeator;
	GameObject temp;
	Queue<GameObject> circles = new Queue<GameObject>();
	int framecount = 0;
	int circlecount = 0;
	int spawn_speed = 70;
	int next_spawn = 70;
	bool is_running = false;
	public GameObject canvas_screen;
	Vector3 newPos;


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
		if (!gameover) {
			audio.clip = loop;
			audio.Play ();
		}
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
		gameover = true;
		menu_music.GetComponent<AudioSource> ().Play ();
	}
	public void unsetRunning() {
		is_running = false;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Stop();
		//wait for Popped Sound to finish .. .so delay Menu Music
		StartCoroutine (WaitForMusic ());
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
						gameObject.GetComponent<Score>().updateScore(
							(int) Mathf.Round(circlecount * score_factor*1.5f)
						);

						Camera.main.GetComponent<AudioSource>().Play();
						Destroy (hit.transform.gameObject);
						// directly spawn new one
						next_spawn = 0;
					}
					else {
						// game over
						gameObject.GetComponent<Gameover>().PopupGameover();
					}
				}
			}
		}
	}

	public void spawnCircle() {

		RectTransform canvas_transform = canvas_screen.GetComponent<RectTransform> ();
		float width = canvas_transform.rect.width;
		float height = canvas_transform.rect.height;

		bool overlay = true;
		while(overlay) {
			overlay=false;

			float x = Random.Range (-(width / 2), width / 2);
			float y = Random.Range ((-height / 2), height / 2);
			newPos = new Vector3 (x, y, 0);

			foreach (GameObject circle in circles) {
				circle.transform.position = new Vector3 (circle.transform.position.x,circle.transform.position.y, 0);
				//Debug.Log(circle.transform.position);
				float circle_collider_radius = circle.GetComponent<CircleCollider2D> ().radius;
				//if ((newPos - circle.transform.position).magnitude > circle_collider_radius + circle.GetComponent<CirclesBehaviour> ().srt_size) {
				//coliision
				if(Mathf.Abs(newPos.x - circle.transform.position.x) < circle_collider_radius + circle.GetComponent<CirclesBehaviour> ().srt_size
				   && Mathf.Abs(newPos.y - circle.transform.position.y) < circle_collider_radius + circle.GetComponent<CirclesBehaviour> ().srt_size){
					overlay = true;
					Debug.Log(newPos + "new vs old " + circle.transform.position);
					Debug.Log("COLLISION XY" + Mathf.Abs(newPos.x - circle.transform.position.x));
					break;
				}
			}
		}

		GameObject temp = Instantiate(Resources.Load("Prefabs/Circle", typeof(GameObject))) as GameObject;
		temp.transform.SetParent(circles_container.transform);
		temp.name = circlecount.ToString();
		
		temp.GetComponent<CirclesBehaviour> ().SpawnCircle (newPos);

		circlecount++;
		temp.GetComponent<CirclesBehaviour>().setNumber(circlecount);
		circles.Enqueue(temp);
		if (loop_is_running && circlecount % 5 == 0) {
			gameObject.GetComponent<AudioSource>().pitch += 0.01f;
		}
	}
}
