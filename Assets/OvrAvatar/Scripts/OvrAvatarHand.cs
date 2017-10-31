using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent (typeof(LineRenderer))]
public class OvrAvatarHand : MonoBehaviour
{
	[SerializeField]
	private OVRInput.Controller _controllerID = OVRInput.Controller.None;

	[SerializeField]
	private OVRInput.RawButton _actionButton;

	[SerializeField]
	private OVRInput.RawButton _cancelButton;

	private LineRenderer _lineRender;

	void Awake() {

		_lineRender = gameObject.GetComponent<LineRenderer> ();
	}

	void Update() {
	
		_lineRender.enabled = (OVRInput.GetActiveController () & _controllerID) != 0;

		if (_lineRender.enabled == false)
			return;

		if (OVRInput.GetUp (_actionButton)) {

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {

				InteractiveItem interactiveItem = hit.collider.GetComponent<InteractiveItem> ();
				if(interactiveItem != null)
					interactiveItem.Click ();
			}
		}

		if (OVRInput.GetUp (_cancelButton)) {

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {

				InteractiveItem interactiveItem = hit.collider.GetComponent<InteractiveItem> ();
				if(interactiveItem != null)
					interactiveItem.Back ();
			}
		}
	}
}