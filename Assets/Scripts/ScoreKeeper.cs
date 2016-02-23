using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static ScoreKeeper instance;

	private int blue_score;
	private int red_score;

	public Text blue_score_text;
	public Text red_score_text;

	// Use this for initialization
	void Start () {
		instance = this;
		blue_score = 0;
		red_score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		blue_score_text.text = "Score: " + blue_score;
		red_score_text.text = "Score: " + red_score;
	}

	public void BlueScored() {
		print ("bluescore");
		blue_score = blue_score + 1;
	}

	public void RedScored() {
		print ("redscore");
		red_score = red_score + 1;
	}
}
