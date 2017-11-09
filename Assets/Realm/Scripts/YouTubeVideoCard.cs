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

	public event System.Action OnPlay;
	public event System.Action OnPause;
	public event System.Action OnComplete;

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

	void OnDestroy() {

		_playback.unityVideoPlayer.targetTexture.Release ();
	}

	void OnPlayPause ()
	{
		if (_playback.unityVideoPlayer.isPlaying) {

			_playback.unityVideoPlayer.Pause ();
			if (OnPause != null)
				OnPause ();
			
		} else {
			
			_playback.unityVideoPlayer.Play ();
			if (OnPlay != null)
				OnPlay ();
		}
	}

	// Use this for initialization
	void Start () {

		_descriptionPanel.Hide ();

		if(_videoURL != null && _videoURL != "") 
			PlayVideo (YouTubeUtils.GetVideoIdFromUrl (_videoURL));
	}
		
	public void PlayVideo(string videoId) {

		if (OnPlay != null)
			OnPlay ();

		_playback.unityVideoPlayer.loopPointReached += OnVideoComplete;

		_playback.PlayYoutubeVideo (videoId);
		_youtubeManager.GetVideoData (videoId, OnVideoDataLoaded);
	}

	private void OnVideoDataLoaded(YoutubeData data) {

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

	void OnVideoComplete (UnityEngine.Video.VideoPlayer source)
	{
		if (OnComplete != null)
			OnComplete ();
	}
}