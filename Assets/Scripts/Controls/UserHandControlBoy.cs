using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;


public class UserHandControlBoy : MonoBehaviour
{
	private Hand hand;
	private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & Hand.AttachmentFlags.SnapOnAttach & Hand.AttachmentFlags.DetachOthers;

	public Hand otherHand;

	public GameObject prefabPlanet;
	private GameObject currentPlanet;

	// Use this for initialization
	void Start()
	{
		hand = GetComponent<Hand>();

		Debug.Log(hand);

		if (hand.startingHandType == Hand.HandType.Right) { 
			SpawnPlanet();
		}
	}
	
	void OnDisable()
	{
		hand.DetachObject(currentPlanet);
		hand.HoverUnlock(currentPlanet.GetComponent<Interactable>());

		Destroy(currentPlanet);
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log(currentPlanet + "IN update");


		HandControls rightHandControls = ControlsManager.Instance.GetControlsFromHand(hand);
		HandControls leftHandControls = ControlsManager.Instance.GetControlsFromHand(otherHand);


		/*
		if (hc.TriggerPulled.Down)
		{
			shoot();
		}
		*/

		Debug.Log(hand.startingHandType);
		float zScale = .1f;
		if (rightHandControls.TriggerPulled.Down)
		{
			Place();
			SpawnPlanet();
		}
		if (rightHandControls.TouchPadPressed.Any)
		{
			if (rightHandControls.TouchPadLocation.y > 0)
			{
				//increase Z (farther away from you)

				Vector3 vec3 = new Vector3(currentPlanet.transform.localPosition.x, currentPlanet.transform.localPosition.y, currentPlanet.transform.localPosition.z + zScale * Time.deltaTime);
				currentPlanet.transform.localPosition = vec3;
			}
			if (rightHandControls.TouchPadLocation.y < 0)
			{
				Vector3 vec3 = new Vector3(currentPlanet.transform.localPosition.x, currentPlanet.transform.localPosition.y, currentPlanet.transform.localPosition.z - zScale * Time.deltaTime);
				currentPlanet.transform.localPosition = vec3;
			}
		}
		
		float scale = .1f;

		if (leftHandControls.TouchPadPressed.Any)
		{
			if (leftHandControls.TouchPadLocation.y > 0)
			{
				Debug.Log(currentPlanet + "IN TOUCHPADPRESSED");
				Vector3 newLocation = new Vector3(currentPlanet.transform.localScale.x + Time.deltaTime * scale, currentPlanet.transform.localScale.y + Time.deltaTime * scale, currentPlanet.transform.localScale.z + Time.deltaTime * scale);
				currentPlanet.transform.localScale = newLocation;
				currentPlanet.GetComponent<Rigidbody>().mass += Time.deltaTime * scale;
				Debug.Log("Scaling up");

			}
			if (leftHandControls.TouchPadLocation.y < 0)
			{
				Vector3 newLocation = new Vector3(currentPlanet.transform.localScale.x + Time.deltaTime * -scale, currentPlanet.transform.localScale.y + Time.deltaTime * -scale, currentPlanet.transform.localScale.z + Time.deltaTime * -scale);
				currentPlanet.transform.localScale = newLocation;
				currentPlanet.GetComponent<Rigidbody>().mass -= Time.deltaTime * scale;

			Debug.Log("Scaling down");
			}
		}


		//if (hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Grip).y > 0)

		//{
			//switch ?!?!??!?
		//}
		
	}

	void SpawnPlanet()
	{
		//instantiate planet
		currentPlanet = Instantiate(prefabPlanet, Vector3.zero, Quaternion.identity);
		Debug.Log(currentPlanet + "IN SPAWNPLANET()");
		currentPlanet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		hand.AttachObject(currentPlanet, this.attachmentFlags);
		hand.HoverLock(currentPlanet.GetComponent<Interactable>());

		Debug.Log(currentPlanet + "AFTER HOVERLOCK");
		currentPlanet.transform.LookAt(hand.transform.position);
		//childs it to the hand
		Debug.Log("Planet Spawned!");
	}

	void Place()
	{
		//detach it from the hand
		Debug.Log("PLACE IS CALLED");
		hand.DetachObject(currentPlanet);
		hand.HoverUnlock(currentPlanet.GetComponent<Interactable>());
	}
}
