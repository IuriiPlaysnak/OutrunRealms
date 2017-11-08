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

	private string _htmlTemplate = string.Empty;

	void Awake() {

		_browser.gameObject.SetActive (false);

		_playButton.OnClick += OnPlayButtonClick;

		TextAsset asset = Resources.Load ("YouTubeVideoTemplate") as TextAsset;
		_htmlTemplate = asset.text;
	}

	void Start() {
		
	}

	void OnPlayButtonClick ()
	{
		if (OutrunRealmDataProvider.isLoadingComlete == false)
			return;

		if (_isPlayerShown == false) {

			_isPlayerShown = true;
			_thumbnail.SetActive (false);
			_description.SetActive (false);
			_browser.gameObject.SetActive (true);

			OutrunRealmDataProvider.VideoData videodata = OutrunRealmDataProvider.videosData.videos [0];
			string videoHTML = _htmlTemplate.Replace ("video_id", videodata.id);
			_browser.LoadHTML(videoHTML);
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
}
