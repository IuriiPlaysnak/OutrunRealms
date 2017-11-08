using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutrunGallery : MonoBehaviour {

	[SerializeField]
	private Image _display;

	[SerializeField]
	private Text _countTextField;

	private List<string> _imagesURLs;

	void Awake() {

		Debug.Assert (_display != null, "Image not found");
		Debug.Assert (_countTextField != null, "Text not found");
	}

	public void SetImages(List<string> imagesURLs) {

		_imagesURLs = imagesURLs;
	}

	public void Show() {
	
		_currentImage = 0;
		LoadImage (_imagesURLs [_currentImage]);
	}

	private int _currentImage;

	public void PrevImage() {

		_currentImage--;
		if (_currentImage < 0)
			_currentImage = _imagesURLs.Count - 1;

		LoadImage (_imagesURLs [_currentImage]);
	}

	public void NextImage() {

		if (_imagesURLs == null || _imagesURLs.Count == 0)
			return;

		_currentImage++;
		if (_currentImage > _imagesURLs.Count - 1)
			_currentImage = 0;

		LoadImage (_imagesURLs [_currentImage]);
	}

	private void LoadImage(string url) {

		_countTextField.text = string.Format ("{0} / {1}", _currentImage + 1, _imagesURLs.Count);

		ResourceManager.LoadImage (url, OnImageLoaded);
	}

	void OnImageLoaded (Texture2D texture)
	{
		_display.sprite = Sprite.Create(
				texture
				, new Rect(0, 0, texture.width, texture.height)
				, Vector2.zero
			);
	}
}