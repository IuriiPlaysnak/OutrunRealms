using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(Collider))]

public class InteractiveItem : MonoBehaviour {

	public event Action OnClick;
	public event Action OnBack;
	public event Action OnOver;
	public event Action OnOut;
	public event Action<RaycastHit> OnMoveOver;

	public bool isClickable {
		get { return OnClick != null; }
	}

	public void Click() {
		ProcessClick ();	
	}

	public void Back() {
		ProcessBack ();
	}

	public void Over() {
		ProcessOver ();
	}

	public void Out() {
		ProcessOut ();
	}

	public void MoveOver(RaycastHit hit) {
		ProcessMoveOver (hit);
	}
		
	private void ProcessBack() {

		if (OnBack != null)
			OnBack ();
	}

	private void ProcessClick() {

		if (OnClick != null)
			OnClick ();
	}

	private void ProcessOver() {

		if (OnOver != null)
			OnOver ();
	}

	private void ProcessMoveOver(RaycastHit hit) {

		if (OnMoveOver != null)
			OnMoveOver (hit);
	}

	private void ProcessOut() {
		if (OnOut != null)
			OnOut ();
	}
}
