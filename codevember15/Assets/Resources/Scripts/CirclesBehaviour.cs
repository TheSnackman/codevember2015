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

		start_size = Random.Range(14, 64);

		int width = Screen.width;
		int height = Screen.height;
		circle_transform.Translate(Random.Range(0, width)-0.5f*width, Random.Range(0, height) - 0.5f*height, 0, Space.World);
		Debug.Log("");

		circle_collider = new CircleCollider2D();
		circle_collider.radius = start_size;
	}
	
	// Update is called once per frame
	void Update () {


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
			if (child.name == "Text"){
				child.GetComponent<Text>().text = number.ToString();
			}
		}
	}

	void OnClick() {

		Destroy(gameObject);
	}
}
