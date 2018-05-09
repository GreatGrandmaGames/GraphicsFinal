using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;


public class UserHandControlBoy : MonoBehaviour
{
	private Hand hand;

	public GameObject prefabPlanet;
	private GameObject currentPlanet;
	
	// Use this for initialization
	void Start ()
	{
		hand = GetComponent<Hand>();
		SpawnPlanet();
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandControls hc = ControlsManager.Instance.GetControlsFromHand(hand);

		/*
		if (hc.TriggerPulled.Down)
		{
			shoot();
		}
		*/
		
		if (hand.startingHandType == Hand.HandType.Right)
		{
			if (hc.TriggerPulled.Down)
			{
				//Place();
				//SpawnPlanet();
			}
			if (hc.TouchPadLocation.y > 0)
			{
				//increase Z (farther away from you)
			}
			if (hc.TouchPadLocation.y < 0)
			{
				//decrease Z (closer to you)
			}
		}

		if (hand.startingHandType == Hand.HandType.Left)
		{
			float scale = .1f;
			if (hc.TouchPadLocation.y > 0)
			{
				Vector3 newLocation = new Vector3(currentPlanet.transform.localScale.x + Time.deltaTime * scale, currentPlanet.transform.localScale.y + Time.deltaTime * scale, currentPlanet.transform.localScale.z + Time.deltaTime * scale);
				currentPlanet.transform.localScale = newLocation;
			}
			if (hc.TouchPadLocation.y < 0)
			{
				Vector3 newLocation = new Vector3(currentPlanet.transform.localScale.x + Time.deltaTime * -scale, currentPlanet.transform.localScale.y + Time.deltaTime * -scale, currentPlanet.transform.localScale.z + Time.deltaTime * -scale);
				currentPlanet.transform.localScale = newLocation;
			}
		}
		if (hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Grip).y > 0)
		{
			//switch ?!?!??!?
		}
	}

	void SpawnPlanet()
	{
		//instantiate planet
		Vector3 vec3 = new Vector3(0, 0, 0);
		currentPlanet = Instantiate(prefabPlanet, vec3, Quaternion.identity);
		hand.AttachObject(currentPlanet);
		hand.HoverLock(currentPlanet.GetComponent<Interactable>());
		//childs it to the hand
		Debug.Log("Planet Spawned!");
	}

	void Place()
	{
		//detach it from the hand
		hand.DetachObject(currentPlanet);
		hand.HoverUnlock(currentPlanet.GetComponent<Interactable>());
	}
}
