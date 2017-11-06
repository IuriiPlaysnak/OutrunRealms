using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunGazeCursor : MonoBehaviour {

	[SerializeField]
	private GameObject _normal;

	[SerializeField]
	private GameObject _timer;

	void Awake() {

		_normal.SetActive (true);
		_timer.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMode(Mode mode) {

		_normal.SetActive (mode == Mode.NORMAL);
		_timer.SetActive (mode == Mode.TIMER);
	}

	public void UpdateTimer(float progress) {

		_timer.GetComponent<Renderer> ().material.SetFloat ("_ColorRampOffset", progress);
	}

	public enum Mode {
		NORMAL,
		TIMER
	}
}
