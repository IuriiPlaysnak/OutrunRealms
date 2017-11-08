using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPanelCard : MonoBehaviour {

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

	public void ShowContent(VideoDescriptionContent content) {

		_title.text = content.title;
		_desription.text = content.description
			.Replace("\r\n", "\n")
			.Replace('\r', '\n')
			.Replace('\n', ' ')
			.Replace("  ", System.Environment.NewLine)
			.Substring (0, 340)
			.Insert (340, "...");
	}

	public enum ContentType {
		VIDEO_DESCRIPTION
	}

	public struct VideoDescriptionContent {
		public string title;
		public string description;
	}
}
