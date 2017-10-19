using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

public class NewsView : MonoBehaviour {

	[SerializeField]
	private Browser _browser;

	[SerializeField]
	private NewsThumbnail _thumbnail;

	private enum ViewMode {

		THUMBNAIL,
		BROWSER
	}

	private ViewMode _viewMode;

	private GameObject _browserGO;
	private GameObject _thumbnailGO;

	void Awake() {

		_viewMode = ViewMode.THUMBNAIL;

		_browserGO = _browser.gameObject;
		_thumbnailGO = _thumbnail.gameObject;

		_thumbnailGO.SetActive (true);
		_browserGO.SetActive (false);

		InteractiveItem interaction = gameObject.GetComponent<InteractiveItem> ();
		if (interaction != null) {
			interaction.OnClick += OnClick;
			interaction.OnBack += OnBack;
		}
	}

	void OnBack ()
	{
		switch (_viewMode) {

		case ViewMode.BROWSER:
			OnBrowserBack ();
			break;

		default:
			break;
		}
	}

	private void OnClick() {

		switch (_viewMode) {

		case ViewMode.THUMBNAIL:
			OnThumbnailClicked ();
			break;

		default:
			break;
		}
	}

	private void OnBrowserBack() {

		_viewMode = ViewMode.THUMBNAIL;

		_browserGO.SetActive (false);
		_thumbnailGO.SetActive (true);
	}

	private void OnThumbnailClicked() {

		if (OutrunRealmSettings.isLoadingComlete == false)
			return;

		_viewMode = ViewMode.BROWSER;

		_browser.LoadURL (OutrunRealmSettings.newsData.link, true);

		_browserGO.SetActive (true);
		_thumbnailGO.SetActive (false);
	}
}