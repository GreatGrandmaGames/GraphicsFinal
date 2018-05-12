using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityWell : MonoBehaviour {

	static float bigG = 100f;

	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();

		rb.useGravity = false;

		GravityManager.Instance.AppendGravityWell (this);
	}

	public Vector3 GetGravitationalForce(Vector3 objPosition, float objMass){

		float dist = Vector3.Distance (transform.position, objPosition);

		float forceAmount = (bigG * rb.mass * objMass) / (dist * dist);

		return new Vector3 (transform.position.x - objPosition.x, transform.position.y - objPosition.y, transform.position.z - objPosition.z) * forceAmount;
	}

	public void AddForce(Vector3 force){
		rb.AddForce (force);
	}
}
