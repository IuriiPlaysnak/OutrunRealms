using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutrunRealmThumbnail : MonoBehaviour {

	protected Text titleTextField;
	protected Image image;

	void Awake() {

		titleTextField = gameObject.GetComponentInChildren<Text> ();
		image = gameObject.GetComponentInChildren<Image> ();

		Debug.Assert (titleTextField != null, "Text field in not found");
		Debug.Assert (image != null, "Image in not found");

		OutrunRealmDataProvider.OnLoadingComplete += OnDataLoaded;
	}

	virtual protected void OnDataLoaded() {

	}

	protected void LoadImage(string url) {

		StartCoroutine (LoadImageCoroutine (url));
	}

	private IEnumerator LoadImageCoroutine(string url) {

		WWW request = new WWW (url);

		while(request.isDone == false) {

			Debug.Log (string.Format ("Image loading: {0}", request.progress));
			yield return request;
		}

		Debug.Log (string.Format ("Image loading: {0}", request.progress));

		image.sprite = Sprite.Create(
			request.texture
			, new Rect(0, 0, request.texture.width, request.texture.height)
			, Vector2.zero
		);
	}
}
