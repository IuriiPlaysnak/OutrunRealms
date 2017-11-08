using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesCard : MonoBehaviour {

	[SerializeField]
	private InteractiveItem _nextButton;

	[SerializeField]
	private InteractiveItem _prevButton;

	[SerializeField]
	private GameObject _description;

	private OutrunGallery _gallery;
	private GalleryAutoplay _autoplay;

	void Awake() {

		_gallery = gameObject.GetComponent<OutrunGallery> ();
		Debug.Assert (_gallery != null, "Gallery is missing");

		_autoplay = gameObject.GetComponent<GalleryAutoplay> ();

		_nextButton.OnClick += OnNextImage;
		_prevButton.OnClick += OnPrevImage;

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if (ii != null) {
			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
		}

		_description.SetActive (false);
	}

	void OnOut ()
	{
		_description.SetActive (false);
	}

	void OnOver ()
	{
		_description.SetActive (true);

		if (_autoplay != null)
			_autoplay.enabled = false;
	}

	void OnPrevImage ()
	{
		_gallery.PrevImage ();
	}

	void OnNextImage ()
	{
		_gallery.NextImage ();
	}

	// Use this for initialization
	void Start () {
		
	}

	private bool _isLoaded;
	void Update () {

		if (_isLoaded)
			return;

		if (OutrunRealmDataProvider.isLoadingComlete == false)
			return;

		_isLoaded = true;
		_gallery.SetImages (OutrunRealmDataProvider.galleryData.images);
		_gallery.Show ();
	}
}
