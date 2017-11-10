using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutrunGallery : MonoBehaviour {

	private const float DEFAULT_AUTOPLAY_DELAY = 5f;

	[SerializeField]
	private RawImage _display;

	[SerializeField]
	private Text _title;

	[SerializeField]
	private Text _description;

	private AutoplayController _autoplay;
	private int _currentImage;

	void Awake() {

		Debug.Assert (_display != null, "ImageDisplay is missing");
		Debug.Assert (_title != null, "Title text field is missing");
		Debug.Assert (_description != null, "Description text field is missing");

		_display.enabled = false;

		_autoplay = gameObject.GetComponent<AutoplayController> ();
		if (_autoplay == null) {
			_autoplay = gameObject.AddComponent<AutoplayController> ();
			_autoplay.delay = DEFAULT_AUTOPLAY_DELAY;
		}

		_autoplay.OnComplete += OnAutoplayComplete;
		_autoplay.Deactivate ();
	}

	void OnAutoplayComplete ()
	{
		NextImage ();
	}

	private List<OutrunRealmDataProvider.ImageData> _images;
	public void SetImages(List<OutrunRealmDataProvider.ImageData> images, bool doShowImmidiatly) {

		_currentImage = 0;
		_images = images;

		if (doShowImmidiatly)
			LoadImage (_images [_currentImage]);
	}

	private void LoadImage(OutrunRealmDataProvider.ImageData data) {

		_title.text = data.title;
		_description.text = data.description;

		_display.enabled = false;

		ResourceManager.LoadImage (data.url, OnImageLoaded);
	}
		
	public void Show() {
	
		LoadImage (_images [_currentImage]);
	}

	public void PrevImage() {

		_autoplay.Deactivate ();

		_currentImage--;
		if (_currentImage < 0)
			_currentImage = _images.Count - 1;

		LoadImage (_images [_currentImage]);
	}

	public void NextImage() {

		if (_images == null || _images.Count == 0)
			return;

		_autoplay.Deactivate ();

		_currentImage++;
		if (_currentImage > _images.Count - 1)
			_currentImage = 0;

		LoadImage (_images [_currentImage]);
	}

	void OnImageLoaded (Texture2D texture)
	{
		_display.texture = texture;
		_display.enabled = true;
		_autoplay.Activate ();
	}
}