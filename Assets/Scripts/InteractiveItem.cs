using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(BoxCollider))]

public class InteractiveItem : MonoBehaviour {

	public event Action OnClick;
	public event Action OnBack;

	public void Click() {
		ProcessClick ();	
	}

	public void Back() {
		ProcessBack ();
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
}
