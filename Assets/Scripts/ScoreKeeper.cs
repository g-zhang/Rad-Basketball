using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static ScoreKeeper instance;

	int blue_score = 0;
	int red_score = 0;

	public Text blue_score_text;
	public Text red_score_text;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		blue_score_text.text = "Score: " + blue_score;
		red_score_text.text = "Score: " + red_score;
	}

	public void BlueScored() {
		blue_score++;
	}

	public void RedScored() {
		red_score++;
	}
}
