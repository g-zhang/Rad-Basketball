using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	public float leftBound;
	public float rightBound;
	public float speed;
	public bool startRight;

	private bool start = true;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		MoveLeftAndRight ();
	}

	void MoveLeftAndRight () {
		if (start) {
			start = false;
			if (startRight)
				GetComponent<Rigidbody2D> ().velocity = Vector2.right * speed;
			else
				GetComponent<Rigidbody2D> ().velocity = Vector2.left * speed;
		}
		if (transform.position.x < leftBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.right * speed;
		else if (transform.position.x > rightBound)
			GetComponent<Rigidbody2D> ().velocity = Vector2.left * speed;
	}
}
