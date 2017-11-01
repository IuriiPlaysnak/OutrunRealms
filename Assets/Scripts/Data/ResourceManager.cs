using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	static public event System.Action<Texture2D> OnImageLoadingComplete;

	static private ResourceManager _instance;

	void Awake() {

		if (_instance == null)
			_instance = this;
		else
			Destroy (this.gameObject);
	}

	static private Dictionary<string, System.Action<Texture2D>> _texturesByCallback;
	static public void LoadImage(string url, System.Action<Texture2D> onLoadigComplete) {

		if (_texturesByCallback == null)
			_texturesByCallback = new Dictionary<string, System.Action<Texture2D>> ();

		_texturesByCallback.Add (url, onLoadigComplete);
		_instance.StartCoroutine (_instance.LoadImageCoroutine (url));
	}

	private IEnumerator LoadImageCoroutine(string url) {

		WWW request = new WWW (url);

		while(request.isDone == false) {
			yield return request;
		}

		if (request.error != null) {
			Debug.LogError (request.error);
		}
		else {
			_texturesByCallback [request.url] (request.texture);
			_texturesByCallback.Remove (request.url);
		}
	}
}