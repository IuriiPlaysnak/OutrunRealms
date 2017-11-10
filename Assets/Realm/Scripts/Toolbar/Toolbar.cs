using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {

	protected const float HIDE_DELAY = .5f;

	protected InteractiveItem _interaction;

	void Awake() {

		Init ();
	}

	virtual protected void Init() {
		
		_interaction = gameObject.GetComponent<InteractiveItem> ();

		if (_interaction != null) {
			_interaction.OnOver += OnToolbarOver;
			_interaction.OnOut += OnToolbarOut;
		}

		Hide (false);
	}

	// Update is called once per frame
	void Update () {

		if (_isWaitingForHide) {
			
			_timer -= Time.deltaTime;
			if (_timer <= 0f)			
				Hide (false);
		}
	}

	private float _timer;
	protected bool _isWaitingForHide;

	virtual public void Show() {

		_isWaitingForHide = false;
		gameObject.SetActive (true);
	}

	virtual public void Hide(bool isDelayed) {

		_isWaitingForHide = isDelayed;

		if (isDelayed)
			_timer = HIDE_DELAY;
		else
			Diactivate ();
	}

	virtual protected void Diactivate() {
		gameObject.SetActive (false);
	}

	virtual protected void OnToolbarOver() {

		Show ();
	}

	virtual protected void OnToolbarOut() {

		Hide (true);
	}
}