using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesCard : MonoBehaviour {

	private const float DEFAULT_AUTOPLAY_DELAY = 5f;

	private const float FULL_TEXT_HEIGHT = 1080;
	private const float SHORT_TEXT_HEIGHT = 200;
	private const float HIDDEN_TEXT_HEIGHT = 0;

	[SerializeField]
	private InteractiveItem _nextButton;

	[SerializeField]
	private InteractiveItem _prevButton;

	[SerializeField]
	private GameObject _description;

	[SerializeField]
	private UnityEngine.UI.Text _autoplayTime;

	private OutrunGallery _gallery;
	private AutoplayController _autoplay;

	void Awake() {

		_gallery = gameObject.GetComponent<OutrunGallery> ();
		Debug.Assert (_gallery != null, "Gallery is missing");
		_gallery.OnImageReady += OnGalleryImageLoaded;

		_nextButton.OnClick += OnNextImage;
		_prevButton.OnClick += OnPrevImage;

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if (ii != null) {
			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
			ii.OnMoveOver += OnMoveOver;
		}

		_autoplay = gameObject.GetComponent<AutoplayController> ();
		if (_autoplay == null) {
			_autoplay = gameObject.AddComponent<AutoplayController> ();
			_autoplay.delay = DEFAULT_AUTOPLAY_DELAY;
		}

		_autoplay.OnComplete += OnAutoplayComplete;
		_autoplay.OnTime += _autoplay_OnTime;
	}
		
	void Start () {

		_autoplay.Stop ();
		StartCoroutine (Init ());
	}

	private IEnumerator Init() {

		while (OutrunRealmDataProvider.isLoadingComlete == false)
			yield return null;

		_gallery.SetImages (OutrunRealmDataProvider.galleryData.images, true);
	}

	void Update () {

		if (_isAnimating)
			AnimationUpdate ();
	}

	private Vector2 _newSize;
	private bool _isAnimating;
	private void AnimateText(float newHeight) {

		_isAnimating = true;
		_newSize = new Vector2 ((_description.transform as RectTransform).sizeDelta.x, newHeight);
	}

	private void AnimationUpdate() {

		(_description.transform as RectTransform).sizeDelta = 
			Vector2.Lerp(
				(_description.transform as RectTransform).sizeDelta
				, _newSize
				, 0.1f
			);

		if (Vector2.Distance ((_description.transform as RectTransform).sizeDelta, _newSize) < 10) {
			(_description.transform as RectTransform).sizeDelta = _newSize;
			_isAnimating = false;
		}
	}

	void OnGalleryImageLoaded ()
	{
		_autoplay.Start ();
	}

	void _autoplay_OnTime (int secondsLeft)
	{
		_autoplayTime.text = string.Format ("Next image in {0} sec", secondsLeft);
	}

	void OnAutoplayComplete ()
	{
		OnNextImage ();
	}

	void OnMoveOver (RaycastHit hit)
	{
		Vector3 localColliderSize;
		Vector3 localHitPoint;

		InteractiveItem.GetLocalHitData (hit, out localColliderSize, out localHitPoint);

		Canvas.ForceUpdateCanvases ();

		float y = (localHitPoint.y + localColliderSize.y / 2) / localColliderSize.y;

		if (y < 0.2f) {
			AnimateText(FULL_TEXT_HEIGHT);

		} else if(y > 0.9f) {

			AnimateText(SHORT_TEXT_HEIGHT);
		}

		Canvas.ForceUpdateCanvases ();
	}

	void OnOut ()
	{
		_autoplay.Resume ();
		AnimateText(HIDDEN_TEXT_HEIGHT);
	}

	void OnOver ()
	{
		_autoplay.Pause ();
		AnimateText(SHORT_TEXT_HEIGHT);
	}

	void OnPrevImage ()
	{
		_autoplay.Stop ();
		_gallery.PrevImage ();
	}

	void OnNextImage ()
	{
		_autoplay.Stop ();
		_gallery.NextImage ();
	}
}
