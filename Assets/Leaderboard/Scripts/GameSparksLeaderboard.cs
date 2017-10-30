using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSparksLeaderboard : MonoBehaviour {

	private const string USER_NAME_ERROR_KEY = "USERNAME";
	private const string USER_NAME_TAKEN_ERROR_VALUE = "TAKEN";

	[SerializeField]
	private string _leaderboardName = "DISTANCE_LEADER";

	[SerializeField]
	private int _leaderboardEntriesCount = 5;

	static private GameSparksLeaderboard _instance;

	public delegate void OnLeaderboardResponse(List<DistanceEntry> leaderboardEntries);
	protected OnLeaderboardResponse onLeaderbpardLoadedCallback;

	private string _displayName = System.Environment.UserName;
	private string _playerName = System.Environment.UserName;
	private string _password = "";

	private bool _isReady;

	static public void GetDistanceLeaderboard(OnLeaderboardResponse callback) {

		_instance.onLeaderbpardLoadedCallback = callback;
		_instance.GetLeaderboard ();
	}

	static public void SavePlayerDistance(int distance) {

		_instance.AddDistanceEntry (distance);
	}

	static public bool isReady {
		get { return _instance._isReady; }
	}

	void Awake() {
		
		if (_instance == null) {

			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else {
			
			Destroy(this.gameObject);
		}
	}

	void Start() {

		_isReady = false;
		Registration ();
	}

	private void Registration() {

		Debug.Log ("Registration");

		new GameSparks.Api.Requests.RegistrationRequest()
			.SetDisplayName(_displayName)
			.SetUserName(_playerName)
			.SetPassword(_password)
			.Send(OnRegistrationResponse);
	}

	private void OnRegistrationResponse(GameSparks.Api.Responses.RegistrationResponse response) {

		if (response.HasErrors) {

			string userNameError = response.Errors.GetString(USER_NAME_ERROR_KEY);

			if (userNameError != null && userNameError == USER_NAME_TAKEN_ERROR_VALUE) {

				Debug.Log(string.Format("OnResistrationResponse: User {0} already exists.", response.DisplayName));
				Authentification ();
			}
			else
				Debug.LogError(string.Format("Registration Error: {0}", response.Errors.JSON));
		}
		else {

			Debug.Log(string.Format("Registration complete: User = {0}", response.DisplayName));
			Authentification();						
		}
	}

	private void Authentification() {

		Debug.Log ("Authentification");

		new GameSparks.Api.Requests.AuthenticationRequest ()
			.SetUserName (_playerName)
			.SetPassword (_password)
			.Send (OnAuthentificationResponse);
	}

	private void OnAuthentificationResponse(GameSparks.Api.Responses.AuthenticationResponse response) {

		Debug.Log ("OnAuthentificationResponse");

		if (response.HasErrors) {

			Debug.LogError(string.Format("Authentification Error: {0}", response.Errors.JSON));
		}
		else {
			
			Debug.Log (string.Format("Authentification complete: User = {0}", response.DisplayName));
			_isReady = true;
		}
	}

	private void GetLeaderboard () {

		if (_isReady == false) 
			ReturnLeaderboardEntries (null);

		new GameSparks.Api.Requests.LeaderboardDataRequest ()
			.SetEntryCount (_leaderboardEntriesCount)
			.SetLeaderboardShortCode (_leaderboardName)
			.Send (OnLeaderboardDataResponse);
	}

	private void OnLeaderboardDataResponse(GameSparks.Api.Responses.LeaderboardDataResponse response) {

		if (response.HasErrors == true) {

			Debug.LogError (response.Errors.JSON);


		} else {

			List<DistanceEntry> entries = new List<DistanceEntry> ();

			foreach (var item in response.Data) {

				DistanceEntry entry = JsonUtility.FromJson<DistanceEntry>(item.JSONString);
				entries.Add (entry);
			}

			ReturnLeaderboardEntries (entries);
		}
	}

	private void ReturnLeaderboardEntries(List<DistanceEntry> entries) {

		if (onLeaderbpardLoadedCallback != null)
			onLeaderbpardLoadedCallback (entries);
	}

	private void AddDistanceEntry(int distance) {

		_isReady = false;

		new GameSparks.Api.Requests.LogEventRequest ()
			.SetEventKey ("MAX_DISTANCE")
			.SetEventAttribute ("DISTANCE", distance)
			.Send (OnSaveResultResponse);
	}

	private void OnSaveResultResponse(GameSparks.Api.Responses.LogEventResponse response) {

		_isReady = true;

		if (response.HasErrors)
			Debug.LogError (string.Format ("OnSaveResultResponse: {0}", response.Errors.JSON));
		else
			Debug.Log ("OnSaveResultResponse: Everything is fine");
	}

	[System.Serializable]
	public struct DistanceEntry {

		public string userName;
		public int rank;
		public int DISTANCE;
	}
}
