using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {

	public GameObject ball;
	public GameObject explosion;
	public AudioClip airHorn;
	public AudioClip ohBabyTriple;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Ball") {
			if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0) {
				int score = 0;
				if (gameObject.tag == "Red_Hoop") {
					ScoreKeeper.instance.RedScored ();
					score = ScoreKeeper.instance.red_score;
				} else if (gameObject.tag == "Blue_Hoop"){
					ScoreKeeper.instance.BlueScored ();
					score = ScoreKeeper.instance.blue_score;
				}

				// Sound Effects and Arena Reset
				if (score == 3)
					GetComponent<AudioSource> ().PlayOneShot (ohBabyTriple);
				else
					GetComponent<AudioSource> ().PlayOneShot (airHorn);

				// boom boom
				Instantiate (explosion, col.gameObject.transform.position, Quaternion.identity);

				HandController[] hands = FindObjectsOfType(typeof(HandController)) as HandController[];
				foreach (HandController hand in hands)
					hand.hasBall = false;

				Destroy (col.gameObject);
				Instantiate(ball, new Vector2 (0, 4), Quaternion.identity);

				GameObject player;
				player = GameObject.Find ("Player_Dog");
				player.transform.localPosition = new Vector2 (7.75f, -5.2f);
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				player = GameObject.Find ("Player_Bunny");
				player.transform.localPosition = new Vector2(9.4f, -5);
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				player = GameObject.Find ("Player_Cat");
				player.transform.localPosition = new Vector2(-20.2f, -5);
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				player = GameObject.Find ("Player_Bird");
				player.transform.localPosition = new Vector2(-22, -5);
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

			}
		}
	}
}
