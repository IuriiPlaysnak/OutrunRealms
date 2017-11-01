using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutrunNewsThumbnail : OutrunRealmThumbnail {

	protected override void OnDataLoaded ()
	{
		base.OnDataLoaded ();
		titleTextField.text = OutrunRealmDataProvider.newsData.title;
		LoadImage (OutrunRealmDataProvider.newsData.imageURL);
	}
}
