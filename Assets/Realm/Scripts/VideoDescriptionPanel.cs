using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoDescriptionPanel : MonoBehaviour {

	[SerializeField]
	private Text _title;

	[SerializeField]
	private Text _desription;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowContent(VideoPanelCard.VideoDescriptionContent content) {

		_title.text = content.title;
		_desription.text = content.description
			.Replace("\r\n", "\n")
			.Replace('\r', '\n')
			.Replace('\n', ' ')
			.Substring (0, 140)
			.Insert (140, "...");
	}

	public void Show() {
		gameObject.SetActive (true);
	}

	public void Hide() {
		gameObject.SetActive (false);
	}
}
