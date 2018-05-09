using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "arrow")
		{
			Debug.Log ("you hit a target!");
			Scoring.playerScore += 10f;
		}
	}

}
