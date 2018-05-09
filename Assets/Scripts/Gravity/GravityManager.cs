using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GravityManager : MonoBehaviour {

	static GravityManager instance;
	 
	List<Rigidbody> toAffect;
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

		//toAffect = new List<Rigidbody>();
		toAffect = Object.FindObjectsOfType<Rigidbody>().ToList();

	}

	public void Affect(Rigidbody rb){
		toAffect.Add (rb);
	}

	
	void FixedUpdate () {

		Debug.Log(toAffect.Count);

		for(int i = toAffect.Count - 1; i >= 0; i--) {


			Rigidbody a = toAffect [i];

			//Removes any destroyed objects
			if (a == null) {
				toAffect.RemoveAt (i);
				continue;
			}

			Debug.Log(a.name);


			Vector3 sumOfForces = new Vector3 ();

			foreach (GravityWell gw in gravityWells) {
				if (a == gw) {
					continue;
				}

				Vector3 force = gw.GetGravitationalForce (a.transform.position, a.GetComponent<Rigidbody>().mass);

				sumOfForces = new Vector3 (sumOfForces.x + force.x, sumOfForces.y + force.y, sumOfForces.z + force.z);
			}

			a.AddForce (sumOfForces);
		}
	}
}
