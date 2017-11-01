using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunGalleryView : MonoBehaviour {

	[SerializeField]
	private OutrunGallery _gallery;
	private GameObject _galleryGO;

	void Awake() {

		_galleryGO = _gallery.gameObject;
		_galleryGO.SetActive (false);

		InteractiveItem interaction = gameObject.GetComponent<InteractiveItem> ();
		if (interaction != null) {
			interaction.OnClick += OnClick;
		}
	}

	private void OnClick() {

		OnThumbnailClicked ();
	}

	private void OnThumbnailClicked() {

		if (OutrunRealmDataProvider.isLoadingComlete == false)
			return;

		_gallery.SetImages (OutrunRealmDataProvider.galleryData.images);
		_galleryGO.SetActive (true);
		_gallery.Show ();
	}
}
