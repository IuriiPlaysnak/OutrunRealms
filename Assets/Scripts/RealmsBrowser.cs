using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class RealmsBrowser : Browser {

	private BrowserInput browserInput;

	new void Awake() {

		base.Awake ();
		browserInput = new BrowserControllerInput (this);
	}

	new protected void HandleInput() {
		Debug.Log ("new handle input");
	}
}