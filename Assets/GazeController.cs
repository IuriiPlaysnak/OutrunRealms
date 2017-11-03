using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour {


	[SerializeField]
	private GameObject _cursor;

	private bool _isFocusing;
	private float _timer;
	private bool _isFocusConsumed;
	private bool _isOver;
	private InteractiveItem _lastInteraction;

	// Update is called once per frame
	void Update () {

		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 50f)) {

			_lastInteraction = hit.collider.GetComponent<InteractiveItem> ();
			Over (_lastInteraction, hit);

			if (_isOver == false) {
				_isOver = true;
			}

			if (_isFocusConsumed == false) {

				if (_isFocusing == false) {

					_isFocusConsumed = false;
					_isFocusing = true;
					_timer = 1f;

				} else {

					_timer -= Time.deltaTime;

					if (_timer <= 0f) {

						_isFocusConsumed = true;
						_timer = 0f;
						Click (_lastInteraction);
					}
				}
			}

			_cursor.GetComponent<Renderer> ().material.SetFloat ("_ColorRampOffset", _timer);

			_cursor.transform.position = hit.point;
			_cursor.transform.rotation = Quaternion.LookRotation (hit.normal);
		}
		else {

			if (_isOver) {
				
				_isOver = false;
				_isFocusing = false;
				_isFocusConsumed = false;
				_cursor.GetComponent<Renderer> ().material.SetFloat ("_ColorRampOffset", 0f);

				Out (_lastInteraction);
				_lastInteraction = null;
			}

			_cursor.transform.position = transform.position + transform.forward * 20f;
			_cursor.transform.rotation = Quaternion.LookRotation (_cursor.transform.position - transform.position);
		}
	}

	private void Click(InteractiveItem item) {

		if(item != null)
			item.Click ();
	}

	private void Over(InteractiveItem item, RaycastHit hit) {
		
		if (item != null)
			item.Over (hit);
	}

	private void Out(InteractiveItem item) {

		if (item != null)
			item.Out ();
	}
}
