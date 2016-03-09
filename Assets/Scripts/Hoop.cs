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
			if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0) {
				if (gameObject.tag == "Red_Hoop") {
					ScoreKeeper.instance.RedScored ();
				} else if (gameObject.tag == "Blue_Hoop"){
					ScoreKeeper.instance.BlueScored ();
				}
				Instantiate (explosion, col.gameObject.transform.position, Quaternion.identity);
				col.gameObject.GetComponent<AudioSource> ().Play ();
				col.gameObject.transform.position = Vector2.zero;
				col.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}
	}

	void Reset () {
		
	}
}
