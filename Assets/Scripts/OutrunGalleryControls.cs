using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunGalleryControls : MonoBehaviour {

	private OutrunGallery _gallery;
	void Awake() {
		_gallery = gameObject.GetComponent<OutrunGallery> ();
	}

	private bool _doSkipScroll;
	private float _timer;

	void Update () {

		if (_doSkipScroll) {
			
			_timer -= Time.deltaTime;

			if (_timer <= 0) 
				_doSkipScroll = false;
			else
				return;
		}

		var mouseScroll = Vector2.zero;

		mouseScroll += OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);
		mouseScroll += OVRInput.Get (OVRInput.Axis2D.SecondaryThumbstick);

		_doSkipScroll = mouseScroll.magnitude > 0;

		if (_doSkipScroll == false)
			return;

		_timer = 1f;

		if (mouseScroll.x < 0)
			_gallery.PrevImage ();
		else if (mouseScroll.x > 0)
			_gallery.NextImage ();
	}
}
