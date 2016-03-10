using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -12.0f) {
			if (gameObject.tag == "Ball") {
				if (gameObject.GetComponent<BallController>().owner == null) {
					gameObject.transform.position = new Vector2 (0, -1); 
					gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				}
			}
			else if (gameObject.tag == "Player" && gameObject.GetComponentInChildren<HandController>().hasBall) {
				gameObject.GetComponentInChildren<HandController> ().disownBall ();
				Vector2 new_pos = new Vector2 (gameObject.transform.position.x, 6.0f);
				gameObject.transform.position = new_pos;
				gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
			else {
				Vector2 new_pos = new Vector2 (gameObject.transform.position.x, 6.0f);
				gameObject.transform.position = new_pos;
				gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}

		}
	}
}