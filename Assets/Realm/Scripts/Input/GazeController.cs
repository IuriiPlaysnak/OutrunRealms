using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour {


	[SerializeField]
	private GameObject _headAnchor;

	[SerializeField]
	private OutrunGazeCursor _cursor;

	private const float CLICK_TIMER = 0.5f;

	private bool _isFocusing;
	private float _timer;
	private bool _isFocusConsumed;
	private bool _isOver;
	private InteractiveItem _lastInteraction;

	private List<RaycastHit> _lastHits = new List<RaycastHit> ();

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
			
//				Debug.Log (ii + "; " + _lastInteraction);
				_isOver = false;
				_timer = 0f;
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

			if (_lastInteraction.isClickable == false) {

				_cursor.UpdateMode (OutrunGazeCursor.Mode.NORMAL);
				return;
			}

			if (_isFocusConsumed == false) {

				if (_isFocusing == false) {

					_isFocusing = true;
					_timer = CLICK_TIMER;

				} else {

					_cursor.UpdateMode (OutrunGazeCursor.Mode.TIMER);

					_timer -= Time.deltaTime;

					if (_timer <= 0f) {

						_isFocusConsumed = true;
						_timer = 0f;
						Click (_lastInteraction);
						_cursor.UpdateMode (OutrunGazeCursor.Mode.NORMAL);
					}
				}

				_cursor.UpdateTimer (_timer);
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

//		Debug.Log ("Out: " + item);

		if (item != null)
			item.Out ();
	}

	private void Over(InteractiveItem item) {

//		Debug.Log ("Over: " + item);

		if (item != null)
			item.Over ();
	}
}
