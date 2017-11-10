using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoplayController : MonoBehaviour {

	[SerializeField]
	private float _delay = 3f;

	public event System.Action OnComplete;
	public event System.Action<float> OnProgress;
	public event System.Action<int> OnTime;
	public event System.Action OnActivated;
	public event System.Action OnDeactivated;
	
	// Update is called once per frame
	void Update () {

		if (_isActivated) {

			_timer -= Time.deltaTime;
			if (_timer <= 0) {

				Deactivate ();
				if (OnComplete != null)
					OnComplete ();
				
			} else {

				if (OnProgress != null)
					OnProgress (_timer / _delay);

				if (OnTime != null)
					OnTime ((int)Mathf.Ceil(_timer));
			}
		}
	}

	private bool _isActivated;
	private float _timer;

	public void Activate() {

		_timer = _delay;
		_isActivated = true;
		if (OnActivated != null)
			OnActivated ();
	}

	public void Deactivate() {
		_isActivated = false;
		if (OnDeactivated != null)
			OnDeactivated ();
	}

	public float delay {
		get { return _delay; }
		set { _delay = value; }
	}
}