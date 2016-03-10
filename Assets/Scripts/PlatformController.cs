using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = Vector2.right * 5;

	}
	
	// Update is called once per frame
	void Update () {
		MoveLeftAndRight (-5.5f, 6);
	}

	void MoveLeftAndRight (float leftBound, float rightBound) {
		if (transform.position.x < leftBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.right * 5;
		else if (transform.position.x > rightBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.left * 5;
	
	}
}
