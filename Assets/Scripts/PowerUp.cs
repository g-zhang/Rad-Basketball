using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	float motionAngle = 0.0f;

	private Vector2 GetBubbleMotion (float motionSpeed, float motionWidth, float motionGravity) {
		motionAngle += Mathf.PI / 180.0f * motionSpeed;
		return new Vector2(Mathf.Sin(motionAngle) * motionWidth, motionGravity); 
	}

	// store original x position
	float originalXPos;

	void Start () {
		originalXPos = transform.position.x;
	}

	void Update () {
		Vector2 driftVelocity = GetBubbleMotion (1.5f, 100.0f, -1.0f);

		// include current Y position to increment gravity. X position must not increment. Use original X if required (as shown above).
		Vector2 bubblePos = new Vector2(originalXPos, transform.position.y);

		transform.position = bubblePos + driftVelocity * Time.deltaTime;

		if (gameObject.transform.position.y < -7.0f) {
			Destroy (this.gameObject);
		}
	}
}
