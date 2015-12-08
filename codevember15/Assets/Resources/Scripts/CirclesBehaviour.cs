using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CirclesBehaviour : MonoBehaviour {

	int val;
	public int lifetime = 0;
	public static int max_lifetime = 80;
	float start_size;
	public float srt_size { get{return start_size;}}
	float max_size;
	RectTransform circle_transform;
	CircleCollider2D circle_collider;

	public void SpawnCircle (Vector3 pos) {

		start_size = 43;

		circle_transform = gameObject.GetComponent<RectTransform>();
		circle_transform.Translate(pos, Space.World);

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
