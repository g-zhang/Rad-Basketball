using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Ball") {
			float ball_ypos = col.gameObject.transform.position.y;
			if (ball_ypos > gameObject.transform.position.y) {
				Instantiate (explosion, col.gameObject.transform.position, Quaternion.identity);
				if (gameObject.tag == "Red_Hoop") {
					ScoreKeeper.instance.RedScored ();
				} else if (gameObject.tag == "Blue_Hoop"){
					ScoreKeeper.instance.BlueScored ();
				}
			}
		}
	}
}
