using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public float scoreToAdd; //varies depending on which level of ring this script is attached to

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "arrow") // when the arrow collides
		{
			Debug.Log ("you hit a target!");
			Scoring.playerScore += scoreToAdd;
		}
	}

}
