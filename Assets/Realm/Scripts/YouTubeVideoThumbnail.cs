using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RawImage))]
public class YouTubeVideoThumbnail : MonoBehaviour {

	public event System.Action<YouTubeVideoThumbnail.Data> OnClick;

	private RawImage _display;
	private YouTubeVideoThumbnail.Data _data;

	void Awake() {

		_display = gameObject.GetComponent<RawImage> ();
		Debug.Assert (_display != null, "Display image is missing");

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();

		if (ii != null) {
			ii.OnClick += 
				() => { 
				if (OnClick != null)
					OnClick (_data); 
			};
		}
	}

	public void SetData(string videoId, int indexInPlaylist, YoutubeTumbnails thumbnails ) {

		_data = new Data () { 
			videoId = videoId,
			videoIndexInPlaylist = indexInPlaylist,
			thumnails = thumbnails
		};

		Load (thumbnails.defaultThumbnail.url);
	}

	private void Load(string url) {

		ResourceManager.LoadImage (url, OnImageLoaded);
	}

	private void OnImageLoaded(Texture2D texture) {
		_display.texture = texture;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public struct Data {

		public int videoIndexInPlaylist;
		public string videoId;
		public YoutubeTumbnails thumnails;
	}
}
