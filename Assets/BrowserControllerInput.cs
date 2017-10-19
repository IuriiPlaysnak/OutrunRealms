using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZenFulcrum.EmbeddedBrowser;

internal class BrowserControllerInput : BrowserInput {

	public BrowserControllerInput (Browser browser):base(browser) {
		
	}

	new public void HandleInput() {

		Debug.Log ("BrowserControllerInput");
	}
}
