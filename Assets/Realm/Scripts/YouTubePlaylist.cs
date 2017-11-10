using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class YouTubePlaylist : MonoBehaviour {

	private const float DEFAULT_AUTOPLAY_DELAY = 3f;

	[SerializeField]
	private string _playlistUrl;

	[SerializeField]
	private int _maxNumberOfItemsInPlaylist = 10;

	[SerializeField]
	private YouTubeVideoPlayer _videoCard;

	[SerializeField]
	private Canvas _thumbnailsCanvas;

	[SerializeField]
	private List<YouTubeVideoThumbnail> _thumbnails;

	private YoutubeAPIManager _youtubeManager;
	private YoutubePlaylistItems[] _playlistItems;
	private AutoplayController _autoplay;

	private int _currentVideoIndex;

	void Awake() {

		_youtubeManager = gameObject.GetComponent<YoutubeAPIManager> ();
		if (_youtubeManager == null)
			_youtubeManager = gameObject.AddComponent<YoutubeAPIManager> ();

		foreach (var thumbnail in _thumbnails) {
			thumbnail.OnClick += OnThumbnailClick;
		}

		_videoCard.OnPause += OnVideoPause;
		_videoCard.OnComplete += OnVideoComplete;
		_videoCard.OnPlay += OnVideoPlay;

		_autoplay = gameObject.GetComponent<AutoplayController> ();
		if (_autoplay != null) {

			_autoplay = gameObject.AddComponent<AutoplayController> ();
			_autoplay.delay = DEFAULT_AUTOPLAY_DELAY;
		}

		_autoplay.OnComplete += OnAutoplayComplete;
	}

	void OnAutoplayComplete ()
	{
		PlayNextVideo ();
	}

	void OnVideoComplete ()
	{
		_thumbnailsCanvas.enabled = true;
		_autoplay.Activate ();
	}

	void OnVideoPlay ()
	{
		_thumbnailsCanvas.enabled = false;
		_autoplay.Deactivate ();
	}

	void OnVideoPause ()
	{
		_thumbnailsCanvas.enabled = true;
	}

	void OnThumbnailClick (YouTubeVideoThumbnail.Data data)
	{
		PlayVideo (data.videoIndexInPlaylist);
	}

	void Start () {

		_autoplay.Deactivate ();
	}

	public void LoadPlaylist(string url) {
		
		_youtubeManager.GetPlaylistItems (
			YouTubeUtils.GetPlaylistIdFromUrl (url)
			, _maxNumberOfItemsInPlaylist
			, OnListData
		);
	}

	void Update() {

		if (Vector3.Distance (gameObject.transform.position, _videoCard.transform.position) > 0.2f) {
			
			gameObject.transform.rotation = _videoCard.transform.rotation;
			gameObject.transform.position = _videoCard.transform.position + gameObject.transform.forward * 0.2f;
		}
	}

	private void OnListData(YoutubePlaylistItems[] playlistItems) {

		_playlistItems = playlistItems;
		PlayVideo (0);
	}

	private void PlayNextVideo() {
		_currentVideoIndex++;
		if (_currentVideoIndex > _playlistItems.Count () - 1)
			_currentVideoIndex = 0;
		PlayVideo (_currentVideoIndex);
	}

	private void PlayVideo(int videoListItemIndex) {

		_currentVideoIndex = videoListItemIndex;
		_videoCard.PlayVideo (_playlistItems [_currentVideoIndex].videoId);

		int nextItemIndex = _currentVideoIndex + 1;

		for (int i = 0; i < _thumbnails.Count; i++) {

			if (nextItemIndex > _playlistItems.Count () - 1)
				nextItemIndex = 0;

			YoutubePlaylistItems item = _playlistItems [nextItemIndex];

			_thumbnails [i].SetData (item.videoId, nextItemIndex, item.snippet.thumbnails);

			nextItemIndex++;
		}
	}
}