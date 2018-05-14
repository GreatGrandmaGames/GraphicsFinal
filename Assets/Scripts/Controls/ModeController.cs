using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Valve.VR.InteractionSystem;

public class ModeController : MonoBehaviour
{
	public ItemPackage longbow;

	public UserHandControlBoy boy;
	public UserHandControlBoy fallbackBoy;
	private UserHandControlBoy boyInUse;

	private GameObject longbowGameObject;
	private GameObject arrowGameObject;

	private bool switched;

	void Start ()
	{

		if (XRSettings.isDeviceActive) {
			boyInUse = boy;

		} else {
			boyInUse = fallbackBoy;

		}

		boyInUse.enabled = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
		HandControls primaryHC = ControlsManager.Instance.GetControlsFromHand (boyInUse.Hand);
		HandControls otherHC = ControlsManager.Instance.GetControlsFromHand (boyInUse.otherHand);

		if (switched == false) {
			if (primaryHC.TouchPadPressed.Any && otherHC.TouchPadPressed.Any) {
				switched = true;

				if (boyInUse.enabled == false)
				{
					DestroyLongbow ();
				} else {
					SpawnLongbow ();
				}

				//This comes after so we can call boyInUse's OnEnabled without worrying the longbow is still attached
				boyInUse.enabled = !boyInUse.enabled;

			}
		} else {

			if (primaryHC.TouchPadPressed.Up || otherHC.TouchPadPressed.Up) {
				switched = false;
			}
		}
	}


	//-------------------------------------------------
	private void SpawnLongbow()
	{

		longbowGameObject = GameObject.Instantiate( longbow.itemPrefab );
		longbowGameObject.SetActive( true );
		boyInUse.Hand.AttachObject( longbowGameObject, Hand.defaultAttachmentFlags, "" );

		if (boyInUse.otherHand != null) {

			arrowGameObject = GameObject.Instantiate (longbow.otherHandItemPrefab);
			arrowGameObject.SetActive (true);
			boyInUse.otherHand.AttachObject (arrowGameObject, Hand.defaultAttachmentFlags);

		}
	}


	private void DestroyLongbow(){

		boyInUse.Hand.DetachObject( longbowGameObject );

		Destroy (longbowGameObject);

		if (boyInUse.otherHand != null) {
			boyInUse.otherHand.DetachObject (arrowGameObject);

			Destroy (arrowGameObject);
		}
	}
}
