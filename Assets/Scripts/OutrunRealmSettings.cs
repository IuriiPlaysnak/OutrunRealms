using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunRealmSettings : MonoBehaviour {

	[SerializeField]
	private string _settingJsonUrl = null;

	public static event System.Action OnLoadingComplete;

	private static OutrunRealmSettings _instance;

	private bool _isLoadingComplete;
	private SettingData _settingsData;

	void Awake() {

		if (_instance == null)
			_instance = this;

		StartCoroutine (_instance.LoadSettingsJSON (_settingJsonUrl));
	}

	// Use this for initialization
	void Start () {

	}

	private IEnumerator LoadSettingsJSON(string url) {

		Debug.Log ("Setting loading...");

		WWW request = new WWW (url);
		yield return request;

		if (request.error != null) {

			Debug.LogError (request.error);

		} else {

			Debug.Log ("Settings loading complete!");

			_settingsData = JsonUtility.FromJson<SettingData> (request.text);
			_isLoadingComplete = true;

			if (OnLoadingComplete != null)
				OnLoadingComplete ();
		}
	}

	static public bool isLoadingComlete {
		get { return _instance._isLoadingComplete; }
	}

	static public NewsData newsData {
		get { return _instance._settingsData.newsData; }
	}

	[System.Serializable]
	private struct SettingData {

		public NewsData newsData;
	}

	[System.Serializable]
	public struct NewsData {

		public string title;
		public string imageURL;
		public string link;

		public override string ToString ()
		{
			return string.Format ("[NewsData]: title = {0}, image = {1}, link = {2} ", title, imageURL, link);
		}
	}
}