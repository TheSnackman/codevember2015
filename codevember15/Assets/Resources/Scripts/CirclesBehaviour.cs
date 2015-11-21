using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CirclesBehaviour : MonoBehaviour {
	
	int id;
	int val;
	float start_size;
	float max_size;
	RectTransform circle_transform;
	CircleCollider2D circle_collider;

	// Use this for initialization
	void Start () {

		circle_transform = gameObject.GetComponent<RectTransform>();

		//start_size = Random.Range(14, 64);
		start_size = 43;

		int width = Screen.width;
		int height = Screen.height;
		int circleHeight = 30;
		circle_transform.Translate(-Random.Range(0, width), -Random.Range(0, height)-circleHeight, 0, Space.World);
		//circle_transform.Translate(Random.Range(0, width)-0.8f*width, Random.Range(0, height) -0.8f*height, 0, Space.World);
		//Debug.Log((Random.Range(0, width)-0.5f*width )+ " " + (Random.Range(0, height) - 0.5f*height));

		gameObject.AddComponent<CircleCollider2D>();
		circle_collider = gameObject.GetComponent<CircleCollider2D>();
		circle_collider.radius = start_size;
	}

	// Returns the current score when
	// this circle is hit by user.
	int getRevenue() {

		return 0;
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

	}
}
