﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardCard : MonoBehaviour {

    public enum CardType { News, Video, Video360, Images, ThreeD, Custom};
    public CardType type;
    public RectTransform canvasDimensions;
    float screenspaceWidth;
    public float GetScreenspaceWidth() { return screenspaceWidth; }
    public void SetScreenspaceWidth(float newWidth) { screenspaceWidth = newWidth; }

	[SerializeField]
	private Toolbar _toolbar;


    void Awake()
    {
        screenspaceWidth = transform.localScale.x * canvasDimensions.rect.width * canvasDimensions.localScale.x;

		if(_toolbar != null)
			_toolbar.Hide (false);
    }

	private bool _isFrontShown = true;

	[SerializeField]
	private Animation _flipAnimation;

	private void Flip() {

		if (_isFrontShown)
		{
			_flipAnimation["flip"].speed = 1;
			_flipAnimation.Play();
			_isFrontShown = false;
		}
		else {
			_flipAnimation["flip"].speed = -1;
			_flipAnimation["flip"].time = _flipAnimation["flip"].length;
			_flipAnimation.Play();
			_isFrontShown = true;
		}
	}

    // Update is called once per frame
    void Update () {
		
	}
}
