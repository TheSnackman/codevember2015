﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {
	
	public GameObject circles_container;
	GameObject temp;
	Queue<GameObject> circles = new Queue<GameObject>();
	int framecount = 0;
	int circlecount = 0;
	float spawn_speed = 100;
	bool is_running = false;
	
	public void setRunning() {
		
		is_running = true;
	}

	// Update is called once per frame
	void Update () {

		if(is_running) {

			//every 100th frame create a circle
			framecount++;
			if(framecount % spawn_speed == 0) {

				spawnCircle(); 

				// increase speed
				if(spawn_speed > 10)
					spawn_speed -= 10;
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
		Debug.Log(circles.Dequeue());
	}
}
