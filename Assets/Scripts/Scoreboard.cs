using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	public Text score;

	
	// Update is called once per frame
	void Update () {
		score.text = Scoring.playerScore.ToString();
	}
}
