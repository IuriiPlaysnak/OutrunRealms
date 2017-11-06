using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryAutoplay : MonoBehaviour {

	[SerializeField]
	private float _delay;


	private OutrunGallery _gallery;
	private float _timer;

	void Awake() {

		_gallery = gameObject.GetComponent<OutrunGallery> ();
		Debug.Assert (_gallery != null, "Gallery is missing");

		_timer = _delay;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		_timer -= Time.deltaTime;
		if (_timer <= 0f) {

			_gallery.NextImage ();
			_timer = _delay;
		}
	}
}
