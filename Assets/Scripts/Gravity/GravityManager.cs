using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GravityManager : MonoBehaviour {

	static GravityManager instance;
	 
	Rigidbody arrow;
	GravityWell[] gravityWells;


	public static GravityManager Instance {
		get {
			return instance;
		}
	}

	void Start () {

		if (instance != null) {
			Debug.LogError ("Cannot have two gravity managers!");
		}

		instance = this;

		gravityWells = Object.FindObjectsOfType<GravityWell> ();
	}

	public void SetArrow(Rigidbody rb){
		this.arrow = rb;
	}

	
	void FixedUpdate () {

		if(arrow != null)
		{
			Vector3 sumOfForces = new Vector3();

			foreach (GravityWell gw in gravityWells)
			{

				Vector3 force = gw.GetGravitationalForce(arrow.transform.position, arrow.mass);

				sumOfForces = new Vector3(sumOfForces.x + force.x, sumOfForces.y + force.y, sumOfForces.z + force.z);
			}

			arrow.AddForce(sumOfForces);
		}
	}
}
