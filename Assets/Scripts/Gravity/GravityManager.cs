using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {
	 
	[SerializeField]
	public List<Rigidbody> toAffect;
	GravityWell[] gravityWells;

	void Start () {

		gravityWells = Object.FindObjectsOfType<GravityWell> ();

	}
	
	void FixedUpdate () {

		foreach (Rigidbody a in toAffect) {

			Vector3 sumOfForces = new Vector3 ();

			foreach (GravityWell gw in gravityWells) {
				if (a == gw) {
					continue;
				}

				Vector3 force = gw.GetGravitationalForce (a.transform.position, a.GetComponent<Rigidbody>().mass);

				sumOfForces = new Vector3 (sumOfForces.x + force.x, sumOfForces.y + force.y, sumOfForces.z + force.z);
			}

			Debug.Log (sumOfForces);
			a.AddForce (sumOfForces);
		}
	}
}
