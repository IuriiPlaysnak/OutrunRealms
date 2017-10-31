using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutrunGallery : MonoBehaviour {

	private Image _image;
	private List<string> _imagesURLs;

	void Awake() {

		_image = gameObject.GetComponentInChildren<Image> ();
		Debug.Assert (_image != null, "Image not found");
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

		Debug.Log ("prev");

		_currentImage--;
		if (_currentImage < 0)
			_currentImage = _imagesURLs.Count - 1;

		LoadImage (_imagesURLs [_currentImage]);
	}

	public void NextImage() {

		Debug.Log ("next");

		_currentImage++;
		if (_currentImage > _imagesURLs.Count - 1)
			_currentImage = 0;

		LoadImage (_imagesURLs [_currentImage]);
	}

	private void LoadImage(string url) {

		StartCoroutine (LoadImageCoroutine (url));
	}

	private IEnumerator LoadImageCoroutine(string url) {

		WWW request = new WWW (url);

		while(request.isDone == false) {

			Debug.Log (string.Format ("Image loading: {0}", request.progress));
			yield return request;
		}

		Debug.Log (string.Format ("Image loading: {0}", request.progress));

		_image.sprite = Sprite.Create(
			request.texture
			, new Rect(0, 0, request.texture.width, request.texture.height)
			, Vector2.zero
		);
	}
}