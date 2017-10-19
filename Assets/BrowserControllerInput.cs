using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

[RequireComponent (typeof(Browser))]
internal class BrowserControllerInput : MonoBehaviour {

	private Browser _browser;
	private Collider _collider;

	void Awake() {

		_browser = gameObject.GetComponent<Browser> ();
		_collider = gameObject.GetComponent<Collider> ();
	}

	void Update() {

		if(_browser.EnableInput)
			HandleControllerInput ();
	}

	private MouseButton prevButtons = 0;
	private Vector2 prevPos;

	[SerializeField]
	private int _scrollSpeed = 100;

	private void HandleControllerInput() {

		GameObject hand = GameObject.Find ("hand_right");

		Ray ray = new Ray (hand.transform.position, hand.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) == false)
			return;

		var mouseScroll = Vector2.zero;

		mouseScroll += OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);
		mouseScroll += OVRInput.Get (OVRInput.Axis2D.SecondaryThumbstick);

		Vector2 mousePos = hit.point.normalized;

		Vector3 localHitPoint = hit.collider.transform.InverseTransformPoint (hit.point);
		Vector3 shiftedHitPoint = localHitPoint + hit.collider.bounds.extents;
		Vector2 hitPoint = new Vector2 (shiftedHitPoint.x, shiftedHitPoint.y);
		mousePos.x = hitPoint.x / hit.collider.bounds.size.x;
		mousePos.y = hitPoint.y / hit.collider.bounds.size.y;

		if (mousePos != prevPos) {
			BrowserNative.zfb_mouseMove(_browser.browserId, mousePos.x, 1 - mousePos.y);
		}

		if (mouseScroll.sqrMagnitude != 0) {
			BrowserNative.zfb_mouseScroll (
				_browser.browserId,
				(int)(mouseScroll.x * _scrollSpeed), (int)(mouseScroll.y * _scrollSpeed)
			);
		}

		if (OVRInput.GetUp (OVRInput.RawButton.A)) {

			Debug.Log ("click");
			BrowserNative.zfb_mouseButton(
				_browser.browserId
				, BrowserNative.MouseButton.MBT_LEFT
				, false
				, 0				
			);
		}

		if (OVRInput.GetDown (OVRInput.RawButton.A)) {

			Debug.Log ("down");
			BrowserNative.zfb_mouseButton(
				_browser.browserId
				, BrowserNative.MouseButton.MBT_LEFT
				, true
				, 2				
			);
		}
			
		prevPos = mousePos;
	}
}