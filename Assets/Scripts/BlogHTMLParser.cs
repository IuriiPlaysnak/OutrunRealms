using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using ZenFulcrum.EmbeddedBrowser;

public class BlogHTMLParser : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Debug.Log ("BlogHTMLParser");
		StartCoroutine("DownloadHTML", "http://playsnak.com/blog.html");

		Browser browser = GameObject.FindObjectOfType<Browser> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator DownloadHTML(string link) {

		Debug.Log ("Loading...");

		WWW request = new WWW (link);
		yield return request;

		Debug.Log ("Loading complete");

		if (request.error != null) {
			
			Debug.LogError (request.error);
		} else {

			ParseHTML(request.text);
		}
	}

	private void ParseHTML(string text) {

		string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments) + @"\text.txt";
		File.WriteAllText (path, text);

		int blogImageIndex = text.IndexOf ("blog-image", 0);

		if (blogImageIndex > -1) {

			int startURLIndex = text.IndexOf ("src=\"", blogImageIndex);
			int endURlIndex = text.IndexOf ("\"", startURLIndex);
			Debug.Log (text.Substring (startURLIndex + 4, endURlIndex));

		} else {
			Debug.LogError ("Image not found");
		}
	}
}
