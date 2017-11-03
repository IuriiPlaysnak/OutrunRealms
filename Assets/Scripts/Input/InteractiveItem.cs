using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(Collider))]

public class InteractiveItem : MonoBehaviour {

	public event Action OnClick;
	public event Action OnBack;
	public event Action<RaycastHit> OnOver;
	public event Action OnOut;

	public void Click() {
		ProcessClick ();	
	}

	public void Back() {
		ProcessBack ();
	}

	public void Over(RaycastHit hit) {
		ProcessOver (hit);
	}

	public void Out() {
		ProcessOut ();
	}
		
	void OnMouseDown(){

		ProcessClick ();
	}

	private void ProcessBack() {

		if (OnBack != null)
			OnBack ();
	}

	private void ProcessClick() {

		if (OnClick != null)
			OnClick ();
	}

	private void ProcessOver(RaycastHit hit) {

		if (OnOver != null)
			OnOver (hit);
	}

	private void ProcessOut() {
		if (OnOut != null)
			OnOut ();
	}
}
