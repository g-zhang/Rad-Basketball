using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	private bool start = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MoveLeftAndRight (-5.5f, 6, true);
	}

	void MoveLeftAndRight (float leftBound, float rightBound, bool startRight) {
		if (start) {
			start = false;
			if (startRight)
				GetComponent<Rigidbody2D> ().velocity = Vector2.right * 5;
			else
				GetComponent<Rigidbody2D> ().velocity = Vector2.left * 5;
		}
		if (transform.position.x < leftBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.right * 5;
		else if (transform.position.x > rightBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.left * 5;
	}
}
