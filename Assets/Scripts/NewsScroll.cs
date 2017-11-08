using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsScroll : MonoBehaviour {

	[SerializeField]
	private RectTransform _textTransform;

	[SerializeField]
	private bool _doScroll = false;

	private bool _isScroll = false;
	private bool _isAnimating = false;

	void Awake() {

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if (ii != null) {
			ii.OnMoveOver += OnMoveOver;
			ii.OnOut += OnOut;
		}
	}

	void OnOut ()
	{
		_doScroll = false;
	}

	void OnMoveOver (RaycastHit hit)
	{
		Vector3 localColliderSize = gameObject.GetComponentInParent<Transform> ().InverseTransformVector (hit.collider.bounds.size);
		Vector3 localHitPoint = gameObject.GetComponentInParent<Transform> ().InverseTransformPoint (hit.point);

		float yK = localHitPoint.y / localColliderSize.y;
		yK /= 0.5f;

		if (yK < -0.3f)
			_doScroll = true;
		else if (hit.point.y > 0.5f)
			_doScroll = false;
	}
	

	private Vector2 _nextPosition;
	private Vector2 _nextSize;

	void Update () {

		if (_isAnimating) {

			_textTransform.anchoredPosition = Vector2.Lerp (_textTransform.anchoredPosition, _nextPosition, 0.1f);
			_textTransform.sizeDelta = Vector2.Lerp (_textTransform.sizeDelta, _nextSize, 0.1f);

			if (Vector2.SqrMagnitude (_textTransform.anchoredPosition - _nextPosition) < 10) {
				
				_textTransform.anchoredPosition = _nextPosition;
				_textTransform.sizeDelta = _nextSize;
				_isAnimating = false;
			}
		}

		if (_doScroll == _isScroll)
			return;

		_isScroll = _doScroll;

		if (_isScroll) {

			_nextPosition = new Vector2(0, -20);
			_nextSize = new Vector2 (450, 530);

		} else {

			_nextPosition = new Vector2(0, -325);
			_nextSize = new Vector2 (450, 215);
		}

		_isAnimating = true;

	}
}
