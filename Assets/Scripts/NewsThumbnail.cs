using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsThumbnail : MonoBehaviour {

	private Text _title;
	private Image _image;

	void Awake() {

		_title = gameObject.GetComponentInChildren<Text> ();
		_image = gameObject.GetComponentInChildren<Image> ();

		Debug.Assert (_title != null, "Text field in not found");
		Debug.Assert (_image != null, "Image in not found");

		OutrunRealmSettings.OnLoadingComplete += OnDataLoaded;
	}

	private void OnDataLoaded() {

		_title.text = OutrunRealmSettings.newsData.title;
		StartCoroutine (LoadImage (OutrunRealmSettings.newsData.imageURL));
	}

	private IEnumerator LoadImage(string url) {

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
