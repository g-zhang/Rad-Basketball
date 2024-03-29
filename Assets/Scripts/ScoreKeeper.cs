﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static ScoreKeeper instance;

	public int blue_score;
	public int red_score;

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
		blue_score = blue_score + 1;
	}

	public void RedScored() {
		red_score = red_score + 1;
	}
}
