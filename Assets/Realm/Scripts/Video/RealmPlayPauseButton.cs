using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmPlayPauseButton : MonoBehaviour {

	private UnityEngine.UI.Text _text;

	void Awake() {

		_text = gameObject.GetComponentInChildren<UnityEngine.UI.Text> ();
		RealmInteractiveItem ii = gameObject.GetComponent<RealmInteractiveItem> ();

		if(ii != null)
			ii.OnClick += OnClick;
	}

	private bool _isPlay;
	void OnClick ()
	{
		_isPlay = !_isPlay;

		if(_isPlay)
			_text.text = "►";
		else
			_text.text = "❚❚";
	}

	// Use this for initialization
	void Start () {

		_text.text = "❚❚";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
