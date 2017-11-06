using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class YouTubeVideoCard : MonoBehaviour {

	[SerializeField]
	private Browser _browser;

	[SerializeField]
	private InteractiveItem _playButton;

	[SerializeField]
	private GameObject _thumbnail;

	[SerializeField]
	private GameObject _description;

	private bool _isPlaying;
	private bool _isPlayerShown;

	void Awake() {

		_browser.gameObject.SetActive (false);

		_playButton.OnClick += OnPlayButtonClick;
	}

	void OnPlayButtonClick ()
	{
		if (_isPlayerShown == false) {

			_isPlayerShown = true;
			_thumbnail.SetActive (false);
			_description.SetActive (false);
			_browser.gameObject.SetActive (true);
			_browser.LoadURL (@"http://localhost:8080/video", true);
		}

		_isPlaying = !_isPlaying;

		StartCoroutine (PlayToggle ());
	}

	private IEnumerator PlayToggle() {

		while (_browser.IsLoaded == false || _browser.IsReady == false)
			yield return null;

		BrowserNative.zfb_mouseButton(
			_browser.browserId
			, BrowserNative.MouseButton.MBT_LEFT
			, true
			, 1				
		);

		BrowserNative.zfb_mouseButton(
			_browser.browserId
			, BrowserNative.MouseButton.MBT_LEFT
			, false
			, 0				
		);
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
