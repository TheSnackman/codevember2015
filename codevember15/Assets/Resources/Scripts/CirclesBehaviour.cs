using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		circle_transform.Translate(Random.Range(0, width)-0.5f*width, Random.Range(0, height) - 0.5f*height, 0, Space.World);

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

		if (Input.touchCount == 1) {

			Debug.Log ("there is a touch");

			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
			if(hit.collider != null) {
				Destroy (gameObject);
			}
				
		}
	}
	
}
