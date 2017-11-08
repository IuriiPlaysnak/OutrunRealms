using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform _follow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

//		this.transform.localPosition = _follow.position;
//		this.transform.localRotation = _follow.rotation;
		transform.rotation = Quaternion.LookRotation(- transform.localPosition);
	}
}
