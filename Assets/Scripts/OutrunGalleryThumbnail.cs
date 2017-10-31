using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunGalleryThumbnail : OutrunRealmThumbnail {

	protected override void OnDataLoaded ()
	{
		base.OnDataLoaded ();

		Debug.Log (OutrunRealmDataProvider.galleryData);

		titleTextField.text = OutrunRealmDataProvider.galleryData.title;
		LoadImage (OutrunRealmDataProvider.galleryData.thumbnailImageURL);
	}
}
