using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public GameObject owner;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		owner = null;

	}

	// Update is called once per frame
	void Update () {
		updateTrail();
        if (owner)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ball"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ball"), false);
        }
    }

	void OnCollisionEnter2D(Collision2D coll) {
		// Instantiate(explosion, transform.position, Quaternion.identity);
		// if (coll.gameObject.tag == "Hoop")
		// Instantiate(explosion, transform.position, Quaternion.identity);

	}

	void updateTrail() {
        // Update Direction
        Vector2 velocity;
        if (owner)
            velocity = Vector2.zero;
        else
            velocity = GetComponent<Rigidbody2D> ().velocity;
        
		Vector2 target = new Vector2(transform.position.x - velocity.normalized.x, transform.position.y - velocity.normalized.y);
		float distance = Vector2.Distance (Vector2.zero, velocity);
		transform.LookAt(target);

		// Update Speed
		GetComponent<ParticleSystem> ().startSpeed = distance / 8;

	}
}
