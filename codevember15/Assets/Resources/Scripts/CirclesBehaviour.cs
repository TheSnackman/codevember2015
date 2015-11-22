﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CirclesBehaviour : MonoBehaviour {

	int val;
	public int lifetime = 0;
	public static int max_lifetime = 80;
	float start_size;
	float max_size;
	RectTransform circle_transform;
	CircleCollider2D circle_collider;

	// Use this for initialization
	void Start () {
		
		start_size = 43;
		
		GameObject canvas_screen = GameObject.Find("Canvas");
		circle_transform = gameObject.GetComponent<RectTransform>();
		RectTransform canvas_transform = canvas_screen.GetComponent<RectTransform>();
		
		float width = canvas_transform.rect.width;
		float height = canvas_transform.rect.height;

		float x = Random.Range(-(width/2), width/2);
		float y = Random.Range((-height/2), height/2);

		/*
		float x = 0.0f;
		float y = 0.0f;
		// check if circles overlay
		bool overlay = true;
		while (overlay) {
			overlay = false;
			x = Random.Range(-(width/2), width/2);
			y = Random.Range((-height/2), height/2);

			Queue<GameObject> circles = GameObject.Find("GameManager").GetComponent<GameLogic>().getCircles();
			foreach(GameObject circle in circles) {
				// check if position collides
				Vector3 pos = circle.GetComponent<RectTransform>().position;
				if(Mathf.Abs(x-pos.x) < 60)
					overlay = true;
				if(Mathf.Abs(y-pos.y) < 60)
					overlay = true;
			}
			Debug.Log(x + " | " + y);
		}*/
		
		circle_transform.Translate(x, y, 0, Space.World);

		gameObject.AddComponent<CircleCollider2D>();
		circle_collider = gameObject.GetComponent<CircleCollider2D>();
		circle_collider.radius = start_size;
	}

	public void setNumber(int number) {

		gameObject.name = number.ToString();
		
		foreach (Transform child in transform) {
			if (child.name == "Text")
				child.GetComponent<Text>().text = number.ToString();
		}
	}

	void Update() {
		// grow
		gameObject.transform.localScale *= 1.01f;

		lifetime++;
		if (lifetime > 80) {
			Gameover go = GameObject.Find("GameManager").GetComponent<Gameover>();
			go.PopupGameover();
		}
	}
}
