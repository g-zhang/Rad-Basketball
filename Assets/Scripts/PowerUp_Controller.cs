using UnityEngine;
using System.Collections;

public class PowerUp_Controller : MonoBehaviour {

	public GameObject p_double_jump;
	public GameObject p_extra_ball;
	public GameObject ball;
	public GameObject extra_ball;

	float counter = 0.0f;
	float checker = 0.0f;

	public static PowerUp_Controller instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void FixedUpdate() {

		checker = Random.Range (1500.0f, 2000.0f);

		if (counter < checker) {
			counter++;
		} else { 

			counter = 0.0f;

			float power_picker = Random.Range (-2.0f, 2.0f);
			if (power_picker < 0) {
				GameObject temp_p = Instantiate(p_double_jump);
				temp_p.transform.position = new Vector2 (Random.Range (-8.0f, 6.0f), 7.5f);
				temp_p.transform.SetParent (this.transform);

			} else {
				GameObject temp_p = Instantiate(p_extra_ball);
				temp_p.transform.position = new Vector2 (Random.Range (-8.0f, 6.0f), 7.5f);
				temp_p.transform.SetParent (this.transform);
			}
		}

	}

	public void SpawnExtraBall () {
		Instantiate (extra_ball);
	}
}