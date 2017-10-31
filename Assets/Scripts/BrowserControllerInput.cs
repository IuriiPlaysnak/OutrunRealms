﻿using System.Collections;
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

//	System.Diagnostics.Process _process;
//	void Start() {
//
//		_process = new System.Diagnostics.Process ();
//		_process.StartInfo.FileName = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
//		_process.StartInfo.Arguments = @"""C:\Users\Iurii Pavliuk\Videos\2_converted to MP4.mp4"" --fullscreen";
//		_process.Start ();
//	}

	void Update() {

		if(_browser.EnableInput)
			HandleControllerInput ();
	}


	[SerializeField]
	private int _scrollSpeed = 100;

	private Vector2 _prevCursorPosition;
	private Vector3 _localHitPointShift = new Vector3 (5f, 0f, 5f);

	private void HandleControllerInput() {

		GameObject hand = GameObject.Find ("hand_right");

		Ray ray = new Ray (hand.transform.position, hand.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) == false)
			return;

		var mouseScroll = Vector2.zero;

		mouseScroll += OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);
		mouseScroll += OVRInput.Get (OVRInput.Axis2D.SecondaryThumbstick);

		Vector2 currentCursorPosition = hit.point.normalized;

		Vector3 localHitPoint = hit.collider.transform.InverseTransformPoint (hit.point);
		Vector3 shiftedHitPoint = localHitPoint;
		shiftedHitPoint.x = _localHitPointShift.x - shiftedHitPoint.x;
		shiftedHitPoint.z = shiftedHitPoint.z + _localHitPointShift.z;

		currentCursorPosition.x = shiftedHitPoint.x / 10f;
		currentCursorPosition.y = shiftedHitPoint.z / 10f;

		if (currentCursorPosition != _prevCursorPosition) {
			BrowserNative.zfb_mouseMove(_browser.browserId, currentCursorPosition.x, currentCursorPosition.y);
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
				, 1				
			);
		}
			
		_prevCursorPosition = currentCursorPosition;
	}
}