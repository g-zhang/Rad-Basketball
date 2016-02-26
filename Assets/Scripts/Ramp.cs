using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Rigidbody2D player_body = coll.gameObject.GetComponent<Rigidbody2D> ();
//			player_body.velocity.y;
		}
	}
}
