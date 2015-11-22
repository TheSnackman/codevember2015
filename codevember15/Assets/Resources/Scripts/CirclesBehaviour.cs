using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CirclesBehaviour : MonoBehaviour {
	
	int id;
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
		
		int circleSize = 60;
		float x = Random.Range(-(width/2), width/2);
		float y = Random.Range((-height/2), height/2);
		
		circle_transform.Translate(x, y, 0, Space.World);

		gameObject.AddComponent<CircleCollider2D>();
		circle_collider = gameObject.GetComponent<CircleCollider2D>();
		circle_collider.radius = start_size;
	}

	public void setNumber(int number) {

		id = number;
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
