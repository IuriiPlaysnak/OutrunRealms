using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {

	private const float SHOW_DELAY = .5f;

	void Awake() {

		foreach (InteractiveItem ii in gameObject.GetComponentsInChildren<InteractiveItem>()) {
			
			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
		}
	}

	// Update is called once per frame
	void Update () {

		if (_timer > 0f) {
			_timer -= Time.deltaTime;

			if (_timer <= 0f) {
				
				Diactivate ();
			}
		}
	}

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
		gameObject.SetActive (true);
	}

	private float _timer;
	public void Hide(bool isDelayed) {

		if (isDelayed)
			_timer = SHOW_DELAY;
		else
			Diactivate ();
	}

	private void Diactivate() {
		gameObject.SetActive (false);
	}
}