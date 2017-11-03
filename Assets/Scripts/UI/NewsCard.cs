using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsCard : MonoBehaviour {

    public Animation flip;
    bool front = true;

	[SerializeField]
	private InteractiveItem _readmoreButton;

	[SerializeField]
	private InteractiveItem _backButton;

	void Awake() {

		_backButton.OnClick += OnClick;
		_readmoreButton.OnClick += OnClick;

	}

	void OnClick() {

		Flip ();
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			Flip ();
        }
	}

	private void Flip() {

		if (front)
		{
			flip["flip"].speed = 1;
			flip.Play();
			front = false;
		}
		else {
			flip["flip"].speed = -1;
			flip["flip"].time = flip["flip"].length;
			flip.Play();
			front = true;
		}
	}
}
