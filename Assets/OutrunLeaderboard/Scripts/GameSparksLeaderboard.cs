using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSparksLeaderboard : MonoBehaviour {

	private const string USER_NAME_ERROR_KEY = "USERNAME";
	private const string USER_NAME_TAKEN_ERROR_VALUE = "TAKEN";

	private enum UserNameSource {
		COMPUTER_NAME,
		OCULUS_USER
	}

	[SerializeField]
	private UserNameSource _userNameAs;

	[SerializeField]
	private string _leaderboardName = "DISTANCE_LEADER";

	[SerializeField]
	private int _leaderboardEntriesCount = 5;

	static private GameSparksLeaderboard _instance;

	public delegate void OnLeaderboardResponse(List<DistanceEntry> leaderboardEntries);
	protected OnLeaderboardResponse onLeaderbpardLoadedCallback;

	private string _displayName;
	private string _playerName;
	private string _password = "";

	private bool _isReady;

	private RealmOculusPlatformManager _oculusPlatformManager;

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

			switch (_userNameAs) {

			case UserNameSource.OCULUS_USER:

				_oculusPlatformManager = gameObject.GetComponent<RealmOculusPlatformManager> ();
				if (_oculusPlatformManager == null)
					_oculusPlatformManager = gameObject.AddComponent<RealmOculusPlatformManager> ();
				
				break;

			case UserNameSource.COMPUTER_NAME:
			default:

				_displayName = System.Environment.UserName;
				_playerName = System.Environment.UserName;
				break;
			}

		} else {
			
			Destroy(this.gameObject);
		}
	}

	void Start() {

		_isReady = false;

		if (_userNameAs == UserNameSource.OCULUS_USER)
			_oculusPlatformManager.LoadData (OnOculusUserData);
		else
			Registration ();
	}

	private void OnOculusUserData(string playerName, string displayName) {
		
		_playerName = playerName;
		_displayName = displayName;
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
