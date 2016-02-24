using UnityEngine;
using System.Collections;

public class PlayerDirectionAnimation : MonoBehaviour {

	SpriteRenderer sprite_renderer;
	Rigidbody2D rigid_body;

	// Use this for initialization
	void Start () {
		sprite_renderer = GetComponent<SpriteRenderer> ();
		rigid_body = transform.parent.gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = rigid_body.velocity;
		if (velocity.x > 0) {
			sprite_renderer.flipX = true;
		} else if (velocity.x < 0) {
			sprite_renderer.flipX = false;
		}
	}
}
