using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmGazeController : MonoBehaviour {


	[SerializeField]
	private GameObject _headAnchor;

	[SerializeField]
	private OutrunGazeCursor _cursor;

	private const float CLICK_TIMER = 0.5f;

	private bool _isFocusing;
	private float _focusTimer;
	private bool _isFocusConsumed;
	private bool _isOver;
	private InteractiveItem _lastInteraction;

	// Update is called once per frame
	void Update () {

		CheckHit ();
	}
		
	private void CheckHit() {

		Ray ray = new Ray (_headAnchor.transform.position, _headAnchor.transform.forward);
		RaycastHit hit;

 		if (Physics.Raycast (ray, out hit, 50f)) {

			_cursor.transform.position = hit.point;
			_cursor.transform.rotation = Quaternion.LookRotation (hit.normal);

			InteractiveItem ii = hit.collider.GetComponent<InteractiveItem> ();

			if (ii == null)
				return;

			if (ii != _lastInteraction) {
			
				_isOver = false;
				_focusTimer = 0f;
				_isFocusConsumed = false;
				_isFocusing = false;
				Out (_lastInteraction);
			}
			
			_lastInteraction = ii;

			MoveOver (_lastInteraction, hit);

			if (_isOver == false) {
				_isOver = true;
				Over (_lastInteraction);
			}

			if (_lastInteraction.doShowCursor == false) {
				_cursor.UpdateMode (OutrunGazeCursor.Mode.INVISIBLE);
				return;
			}

			if(_lastInteraction.isClickable == false) {
				_cursor.UpdateMode (OutrunGazeCursor.Mode.NORMAL);
				return;
			}

			if (_isFocusConsumed == false) {

				if (_isFocusing == false) {

					_isFocusing = true;
					_focusTimer = CLICK_TIMER;

				} else {

					_cursor.UpdateMode (OutrunGazeCursor.Mode.TIMER);

					_focusTimer -= Time.deltaTime;

					if (_focusTimer <= 0f) {

						_isFocusConsumed = true;
						_focusTimer = 0f;
						Click (_lastInteraction);
						_cursor.UpdateMode (OutrunGazeCursor.Mode.NORMAL);
					}
				}

				_cursor.UpdateTimer (_focusTimer);
			}

		} else {

			_cursor.UpdateMode (OutrunGazeCursor.Mode.NORMAL);
			_cursor.transform.position = _headAnchor.transform.position + _headAnchor.transform.forward * 10f;
			_cursor.transform.rotation = Quaternion.LookRotation (_cursor.transform.position - _headAnchor.transform.position);

			if (_isOver) {
				
				_isOver = false;
				_isFocusing = false;
				_isFocusConsumed = false;
				_cursor.UpdateTimer (1);

				Out (_lastInteraction);
				_lastInteraction = null;
			}
		}
	}

	private void Click(InteractiveItem item) {

		if(item != null)
			item.Click ();
	}

	private void MoveOver(InteractiveItem item, RaycastHit hit) {
		
		if (item != null)
			item.MoveOver (hit);
	}

	private void Out(InteractiveItem item) {

		if (item != null)
			item.Out ();
	}

	private void Over(InteractiveItem item) {

		if (item != null)
			item.Over ();
	}
}
