﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarButton : MonoBehaviour {

	private const float HIDE_DELAY = .2f;

	[SerializeField]
	private List<InteractiveItem> _relatives;

	void Awake() {

		InteractiveItem interaction = gameObject.GetComponent<InteractiveItem> ();

		if (interaction != null) {
			interaction.OnOver += OnOver;
			interaction.OnOut += OnOut;
		}

		foreach (var relative in _relatives) {
			relative.OnOver += OnOver;
			relative.OnOut += OnOut;
		}

		Hide (false);
	}

	void OnOver() {
		Show ();
	}

	void OnOut() {
		Hide (true);
	}

	public void ChangeParent(RectTransform parent) {

		transform.parent = parent;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;

		Show ();
	}

	public void ReleaseFromParent() {
		Hide (true);
	}

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
}
