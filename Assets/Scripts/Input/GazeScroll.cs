using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeScroll : MonoBehaviour {

	[SerializeField]
	private ScrollRect _scroll;

	[Range(0f, 1f)]
	public float position = 0f;

//	private Vector3 _colliderSize;

	void Awake() {

		_scroll.verticalNormalizedPosition = 1f;

//		_colliderSize = gameObject.GetComponentInParent<Transform> ().InverseTransformVector (hit.collider.bounds.size);

		InteractiveItem ii = gameObject.GetComponent<InteractiveItem> ();
		if(ii != null)
			ii.OnMoveOver += OnMoveOver;
	}

	void OnMoveOver (RaycastHit hit)
	{
		Vector3 localColliderSize = gameObject.GetComponentInParent<Transform> ().InverseTransformVector (hit.collider.bounds.size);
		Vector3 localHitPoint = gameObject.GetComponentInParent<Transform> ().InverseTransformPoint (hit.point);

		float yK = localHitPoint.y / localColliderSize.y;
		yK /= 0.5f;

		if (Mathf.Abs(yK) < 0.1f)
			return;

		if (yK > 0)
			_scroll.verticalNormalizedPosition = Mathf.Min (_scroll.verticalNormalizedPosition + yK * 0.02f, 1f);
		else if(yK < 0)
			_scroll.verticalNormalizedPosition = Mathf.Max (_scroll.verticalNormalizedPosition + yK * 0.02f, 0f);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {

//		Canvas.ForceUpdateCanvases ();
//		_scroll.verticalNormalizedPosition = position;
//		Canvas.ForceUpdateCanvases ();

	}
}
