using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesCard : MonoBehaviour {

	[SerializeField]
	private InteractiveItem _nextButton;

	[SerializeField]
	private InteractiveItem _prevButton;

	[SerializeField]
	private GameObject _description;

	private OutrunGallery _gallery;
	private GalleryAutoplay _autoplay;

	void Awake() {

		_gallery = gameObject.GetComponent<OutrunGallery> ();
		Debug.Assert (_gallery != null, "Gallery is missing");

		_autoplay = gameObject.GetComponent<GalleryAutoplay> ();

		_nextButton.OnClick += OnNextImage;
		_prevButton.OnClick += OnPrevImage;

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if (ii != null) {
			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
			ii.OnMoveOver += OnMoveOver;
		}
	}

	void OnMoveOver (RaycastHit hit)
	{
		Vector3 localColliderSize;
		Vector3 localHitPoint;

		InteractiveItem.GetLocalHitData (hit, out localColliderSize, out localHitPoint);

		Canvas.ForceUpdateCanvases ();

		float y = (localHitPoint.y + localColliderSize.y / 2) / localColliderSize.y;

		if (y < 0.2f) {
			AnimateText(1080);

		} else if(y > 0.9f) {
			
			AnimateText(200);
		}

		Canvas.ForceUpdateCanvases ();
	}

	void OnOut ()
	{
		AnimateText(0);
		if (_autoplay != null)
			_autoplay.enabled = true;
	}

	void OnOver ()
	{
		AnimateText(200);
		if (_autoplay != null)
			_autoplay.enabled = false;
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

	void OnPrevImage ()
	{
		_gallery.PrevImage ();
	}

	void OnNextImage ()
	{
		_gallery.NextImage ();
	}

	// Use this for initialization
	void Start () {
		
	}

	private bool _isLoaded;
	void Update () {

		if (_isAnimating)
			AnimationUpdate ();

		if (_isLoaded)
			return;

		if (OutrunRealmDataProvider.isLoadingComlete == false)
			return;

		_isLoaded = true;
		_gallery.SetImages (OutrunRealmDataProvider.galleryData.images, true);
	}
}
