using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouTubeVideoCard : MonoBehaviour {

	[SerializeField]
	private string _videoURL;

	[SerializeField]
	private VideoPanelCard _contentPanel;

	[SerializeField]
	private VideoDescriptionPanel _descriptionPanel;

	[SerializeField]
	private List<InteractiveItem> _playPauseButtons;

	private YouTubePlayback _playback;
	private YoutubeAPIManager _youtubeManager;
	private InteractiveItem _interaction;

	void Awake() {

		_interaction = gameObject.GetComponent<InteractiveItem> ();

		_playback = gameObject.GetComponentInChildren<YouTubePlayback> ();
		Debug.Assert (_playback != null, "Playback is missing");

		_youtubeManager = gameObject.GetComponent<YoutubeAPIManager> ();
		if (_youtubeManager == null)
			_youtubeManager = gameObject.AddComponent<YoutubeAPIManager> ();
	
		foreach(var button in _playPauseButtons)
			button.OnClick += OnPlayPause;
	}

	void OnPlayPause ()
	{
		if (_playback.unityVideoPlayer.isPlaying)
			_playback.unityVideoPlayer.Pause ();
		else
			_playback.unityVideoPlayer.Play ();
	}

	// Use this for initialization
	void Start () {

		if (_interaction != null) {
			_interaction.OnOver += OnOver;
			_interaction.OnOut += OnOut;
		}

		_descriptionPanel.Hide ();
		_playback.PlayYoutubeVideo (GetVideoIdFromUrl (_videoURL));
		_youtubeManager.GetVideoData (GetVideoIdFromUrl (_videoURL), OnVideoData);
	}

	void OnOut ()
	{
//		_descriptionPanel.Show ();
//		_playback.unityVideoPlayer.Pause ();
	}

//	private bool _isLoaded;
	void OnOver ()
	{
//		_descriptionPanel.Hide ();
//
//		if (_isLoaded)
//			_playback.unityVideoPlayer.Play ();
//		else {
//
//			_isLoaded = true;
//			_playback.PlayYoutubeVideo (GetVideoIdFromUrl (_videoURL));
//		}
	}

	private void OnVideoData(YoutubeData data) {

		_contentPanel.ShowContent (
			new VideoPanelCard.VideoDescriptionContent () 
			{
				title =  data.snippet.title
				, description = data.snippet.description
			}
		);

		_descriptionPanel.ShowContent (
			new VideoPanelCard.VideoDescriptionContent () 
			{
				title =  data.snippet.title
				, description = data.snippet.description
			}
		);
	}

	// Update is called once per frame
	void Update () {
		
	}

	private string GetVideoIdFromUrl(string url) {
		
		string result = url.Remove (0, url.IndexOf ("watch?v="));
		result = result.Replace ("watch?v=", "");
		int indexOfIdEnd = result.IndexOf ("&");
		if (indexOfIdEnd > -1)
			result = result.Substring (0, indexOfIdEnd);

		return result;
	}
}
