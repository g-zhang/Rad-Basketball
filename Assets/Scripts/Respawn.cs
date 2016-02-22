using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -12.0f) {
			gameObject.transform.position = Vector2.zero;
		}
	}
}
