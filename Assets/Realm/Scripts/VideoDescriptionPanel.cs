using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoDescriptionPanel : MonoBehaviour {

	private const int MAX_DESCRIPTION_TEXT_LENGTH = 140;

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

	public void ShowContent(RealmVideoContentCard.VideoDescriptionContent content) {

		_title.text = content.title;
		_desription.text = FormatDescription (content.description);
	}

	private string FormatDescription(string text) {

		string result = text
			.Replace ("\r\n", "\n")
			.Replace ('\r', '\n')
			.Replace ('\n', ' ')
			.Replace ("  ", System.Environment.NewLine);

		if(text.Length > MAX_DESCRIPTION_TEXT_LENGTH) {

			result = result
				.Substring (0, MAX_DESCRIPTION_TEXT_LENGTH)
				.Insert (MAX_DESCRIPTION_TEXT_LENGTH, "...");
		}

		return result;
	}

	public void Show() {
		gameObject.SetActive (true);
	}

	public void Hide() {
		gameObject.SetActive (false);
	}
}
