using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class BrowserGazeController : MonoBehaviour {

	private Browser _browser;

	void Awake() {

		_browser = gameObject.GetComponent<Browser> ();

		InteractiveItem item = gameObject.GetComponent<InteractiveItem> ();
		item.OnClick += OnClick;
		item.OnOut += OnOut;
		item.OnMoveOver += OnMoveOver;
	}

	void OnMoveOver (RaycastHit hit)
	{
		BrowserNative.zfb_mouseMove(_browser.browserId, hit.textureCoord.x, 1 - hit.textureCoord.y);
	}

	void OnOut ()
	{
		
	}

	void OnClick ()
	{
		Debug.Log ("down");
		BrowserNative.zfb_mouseButton(
			_browser.browserId
			, BrowserNative.MouseButton.MBT_LEFT
			, true
			, 1				
		);

		Debug.Log ("click");
		BrowserNative.zfb_mouseButton(
			_browser.browserId
			, BrowserNative.MouseButton.MBT_LEFT
			, false
			, 0				
		);
	}
}