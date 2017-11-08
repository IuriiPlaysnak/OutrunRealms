using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInteractivity : MonoBehaviour {

	void Awake() {

		InteractiveItem interaction = this.gameObject.GetComponent<InteractiveItem> ();
		if (interaction != null) {
			interaction.OnBack += OnBack;
		}
	}

	void OnBack ()
	{
		gameObject.SetActive (false);
	}
}