﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouTubePlaylistAutoplayIndicator : MonoBehaviour {

	private UnityEngine.UI.Text _text;

	void Awake() {

		AutoplayController autoplay = gameObject.GetComponentInParent<AutoplayController> ();

		if (autoplay != null) {
			autoplay.OnComplete += OnAutoplayComplete;
			autoplay.OnTime += OnAutoplayTime;
			autoplay.OnStarted += OnAutoplayActivated;
			autoplay.OnStopped += OnAutoplayDeactivated;
		}

		_text = gameObject.GetComponentInChildren<UnityEngine.UI.Text> ();
	}

	void OnAutoplayDeactivated ()
	{
		gameObject.SetActive (false);
	}

	void OnAutoplayActivated ()
	{
		gameObject.SetActive (true);
	}

	void OnAutoplayTime (int secondsLeft)
	{
		_text.text = string.Format ("Next video in {0} sec", secondsLeft);
	}

	void OnAutoplayComplete ()
	{
		Debug.Log ("OnAutoplayComplete");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}