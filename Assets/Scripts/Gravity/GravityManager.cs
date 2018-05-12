using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GravityManager : MonoBehaviour {

	static GravityManager instance;
		 
	Rigidbody[] arrow;
	List<GravityWell> gravityWells;


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

		gravityWells = Object.FindObjectsOfType<GravityWell> ().ToList();
	}

	public void SetArrow(Rigidbody[] rb){
		this.arrow = rb;
	}

	public void AppendGravityWell(GravityWell gw){
		this.gravityWells.Add (gw);
	}

	
	void FixedUpdate () {
		if (arrow != null) {
			foreach (Rigidbody a in arrow) {
				Vector3 sumOfForces = new Vector3 ();

				foreach (GravityWell gw in gravityWells) {

					Vector3 force = gw.GetGravitationalForce (a.transform.position, a.mass);

					sumOfForces = new Vector3 (sumOfForces.x + force.x, sumOfForces.y + force.y, sumOfForces.z + force.z);
				}

				Debug.Log (sumOfForces);
				a.AddForce (sumOfForces);
			}
		}
	}
}
