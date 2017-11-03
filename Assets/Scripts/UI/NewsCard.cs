using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsCard : MonoBehaviour {

    public Animation flip;
    bool front = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
}
