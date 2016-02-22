using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {

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
				if (gameObject.tag == "Red_Hoop") {
					print ("Red was scored on");
					ScoreKeeper.instance.BlueScored ();
				} else if (gameObject.tag == "Blue_Hoop"){
					print ("Blue was scored on");
					ScoreKeeper.instance.RedScored ();
				}
			}
		}
	}
}
