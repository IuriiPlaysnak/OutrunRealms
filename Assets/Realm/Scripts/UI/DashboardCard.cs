using System.Collections;
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

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if (ii != null) {

			ii.OnOver += OnOver;
			ii.OnOut += OnOut;
		}

		if(_toolbar != null)
			_toolbar.Hide (false);
    }

	void OnOut ()
	{
//		Debug.Log(this + ": OnOut");
//		if(_toolbar != null)
//			_toolbar.Hide (true);
//
//		iTween.MoveBy(
//			gameObject, 
//			iTween.Hash(
//				"position", Vector3.one * -0.5f, 
//				"space", Space.Self,
//				"time", 0.5f
//			)
//		);
	}

	void OnOver ()
	{
//		Debug.Log(this + ": OnOver");
//		if(_toolbar != null)
//			_toolbar.Show();
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
