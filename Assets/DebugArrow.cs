using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GravityManager.Instance.SetArrow (GetComponentsInChildren<Rigidbody> ());
	}
}
