using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	// fields set in the Unity Inspector pane
	public float velocityMult = 20f;
	public GameObject projectile;
	public bool _____________________________;
	// fields set dynamically
	public Vector2 launchPos;
	public bool aimingMode;

	void Update() {
		// If Slingshot is not in aimingMode, don't run this code
		if (!aimingMode) return;
		// Get the current mouse position in 2D screen coordinates
		Vector2 mousePos2D = Input.mousePosition;
		// Convert the mouse position to 3D world coordinates
		Vector2 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
		// Find the delta from the launchPos to the mousePos3D
		Vector2 mouseDelta = mousePos3D - launchPos;
		// Limit mouseDelta to the radius of the Slingshot SphereCollider
		float maxMagnitude = this.GetComponent<CircleCollider2D>().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}
		// Move the projectile to this new position
		Vector2 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp(0)) {
			// The mouse has been released
			aimingMode = false;
			projectile.GetComponent<Rigidbody2D>().isKinematic = false;
			projectile.GetComponent<Rigidbody2D>().velocity = -mouseDelta * velocityMult;
		}
	}

	void OnMouseDown() {
		aimingMode = true;
		launchPos = transform.position;
		projectile.GetComponent<Rigidbody2D>().isKinematic = true;
	}
}
