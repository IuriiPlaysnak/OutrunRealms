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

		ResourceManager.OnImageLoadingComplete += OnImageLoaded;
		ResourceManager.LoadImage (url, OnImageLoaded);
	}

	void OnImageLoaded (Texture2D texture)
	{
		image.sprite = Sprite.Create(
			texture
			, new Rect(0, 0, texture.width, texture.height)
			, Vector2.zero
		);
	}
}