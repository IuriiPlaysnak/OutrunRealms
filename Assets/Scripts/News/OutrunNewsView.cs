using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

public class OutrunNewsView : MonoBehaviour {

	[SerializeField]
	private Browser _browser;
	private GameObject _browserGO;

	void Awake() {

		_browserGO = _browser.gameObject;
		_browserGO.SetActive (false);

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

		_browser.LoadURL (OutrunRealmDataProvider.newsData.link, true);
		_browserGO.SetActive (true);
	}
}