using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarWithMovingButtons : MonoBehaviour {

	private const float SHOW_DELAY = .2f;

	private InteractiveItem _interaction;

	[SerializeField]
	private List<GameObject> _buttons;

	private enum Orientation {
		VERTICAL,
		HORIZONTAL
	}

	[SerializeField]
	private Orientation _orientation;

	void Awake() {

		_interaction = gameObject.GetComponent<InteractiveItem> ();

		if (_interaction != null) {
			_interaction.OnMoveOver += OnMoveOver;
		}

		foreach (InteractiveItem ii in gameObject.GetComponentsInChildren<InteractiveItem>()) {

			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
		}

		Hide (false);
	}

	void OnMoveOver (RaycastHit hit)
	{
		Vector3 localColliderSize;
		Vector3 localHitPoint;

		InteractiveItem.GetLocalHitData (hit, out localColliderSize, out localHitPoint);

		switch (_orientation) {

		case Orientation.VERTICAL:
			SetVerticalPosition (localHitPoint.y + localColliderSize.y / 2);
			break;

		case Orientation.HORIZONTAL:
		default:
			SetHorizontalPosition (localHitPoint.x + localColliderSize.x / 2);
			break;
		}
	}

	private void SetVerticalPosition(float newY) {

		Debug.Log (newY);

		Vector2 newPosition = new Vector2 ();

		foreach (var button in _buttons) {

			RectTransform buttonTransform = button.transform as RectTransform;
			newPosition.x = buttonTransform.anchoredPosition.x;
			newPosition.y = newY;

			buttonTransform.anchoredPosition = newPosition;
		}
	}

	private void SetHorizontalPosition(float newX) {

		Vector2 newPosition = new Vector2 ();

		foreach (var button in _buttons) {

			RectTransform buttonTransform = button.transform as RectTransform;
			newPosition.x = newX;
			newPosition.y = buttonTransform.anchoredPosition.y;

			buttonTransform.anchoredPosition = newPosition;
		}
	}

	void Update () {

		if (_timer > 0f) {
			_timer -= Time.deltaTime;

			if (_timer <= 0f) {
				Diactivate ();
			}
		}
	}

	void OnToolbarOver() {

		Show ();
	}

	void OnToolbarOut() {

		Hide (true);
	}

	private bool _isOverButton;

	void OnOut ()
	{
		Hide (true);
	}

	void OnOver ()
	{
		Show ();
	}

	public void Show() {

		_timer = -1f;
		foreach (var button in _buttons) 
			button.SetActive (true);
	}

	private float _timer;
	public void Hide(bool isDelayed) {

		if (isDelayed)
			_timer = SHOW_DELAY;
		else
			Diactivate ();
	}

	private void Diactivate() {
		foreach (var button in _buttons) {
			button.SetActive (false);
		}
	}
}
