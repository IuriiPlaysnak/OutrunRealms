using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunRealmDataProvider : MonoBehaviour {

	private enum SourceFormat
	{
		JSON,
		HTML
	}

	[SerializeField]
	private SourceFormat _sourceFormat;

	[SerializeField]
	private string _settingJsonUrl = null;

	[SerializeField]
	private string _webSiteUrl = null;



	public static event System.Action OnLoadingComplete;

	private static OutrunRealmDataProvider _instance;

	public static OutrunRealmDataProvider instance {
		get {
			return _instance;
		}
	}

	private bool _isLoadingComplete;
	private SettingData _settingsData;
	private IOutrunRealmDataSource _dataSource;

	void Awake() {

		if (_instance == null)
			_instance = this;

		string dataSourceUrl = string.Empty;

		switch (_sourceFormat) {

			case SourceFormat.JSON:

				_dataSource = new OutrunRealmJSONdataSource();
				dataSourceUrl = _settingJsonUrl;
				break;

			case SourceFormat.HTML:

				_dataSource = new OutrunRealmHTMLDataSource ();
				dataSourceUrl = _webSiteUrl;
				break;

			default:
				_dataSource = new OutrunRealmHTMLDataSource ();
				break;
		}

		_dataSource.OnLoadingComplete += OnDataLoaded;
		_dataSource.Load (dataSourceUrl);
	}

	private void OnDataLoaded (SettingData data)
	{
		_settingsData = data;

		_isLoadingComplete = true;

		if (OnLoadingComplete != null)
			OnLoadingComplete ();
	}

	static public bool isLoadingComlete {
		get { return _instance._isLoadingComplete; }
	}

	static public NewsData newsData {
		get { return _instance._settingsData.newsData; }
	}

	[System.Serializable]
	public struct SettingData {

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