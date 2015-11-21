﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {
	
	public GameObject circles_container;
	GameObject temp;
	Queue<GameObject> circles = new Queue<GameObject>();
	int framecount = 0;
	int circlecount = 0;
	int spawn_speed = 70;
	int next_spawn = 70;
	bool is_running = false;
	
	public void setRunning() {
		is_running = true;
	}

	public Queue<GameObject> getCircles () {
		return circles;
	}

	// Update is called once per frame
	void Update () {

		if(is_running) {
			framecount++;

			// create a new circle every spawn_speed frame
			if(next_spawn == 0) {
				spawnCircle();
				next_spawn = spawn_speed;
				if (spawn_speed > 30)
					spawn_speed--;
			}

			next_spawn--;

			// check if there is a touchinput hitting
			if (Input.touchCount == 1) {
				//Debug.Log ("touchinput");
				
				//Vector3 pos = Camera.main.ViewportToScreenPoint(Input.GetTouch(0).position);
				RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, Vector2.zero);
				
				if(hit.collider != null){
					//Debug.Log(hit.transform.gameObject.name);

					// check whether hit object is first one in queue
					GameObject temp = circles.Dequeue();

					if(temp.name.Equals(hit.transform.gameObject.name)) {
						//Debug.Log("right one");
						Destroy (hit.transform.gameObject);
						// directly spawn new one
						next_spawn = 0;
						// get points
						GameObject.Find("GameManager").GetComponent<Score>().updateScore(spawn_speed);
						//Debug.Log(score);
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
		circlecount++;
		GameObject temp = Instantiate(Resources.Load("Prefabs/Circle", typeof(GameObject))) as GameObject;
		temp.transform.SetParent(circles_container.transform);
		temp.name = circlecount.ToString();
		temp.GetComponent<CirclesBehaviour>().setNumber(circlecount);
		circles.Enqueue(temp);
		//Debug.Log(circles.Dequeue());
	}
}