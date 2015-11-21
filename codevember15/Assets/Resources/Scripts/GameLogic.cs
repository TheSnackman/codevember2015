using UnityEngine;
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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(is_running) {
			//every frame wird circle erstellt
			framecount++;

			if(framecount % 100 == 0)
				spawnCircle(); 

			// let every circle grow

		}
	}

	public void setRunning() {

		is_running = true;
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
