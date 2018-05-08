using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StartWithForce : MonoBehaviour {

	public Vector3 startForce;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody> ().AddForce (startForce);
	}
}
