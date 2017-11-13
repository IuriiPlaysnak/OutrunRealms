using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchLeaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


	private bool _isFetched;
	void Update () {

		if (_isFetched == false) {

			if (GameSparksLeaderboard.isReady) {

				_isFetched = true;
				GameSparksLeaderboard.GetDistanceLeaderboard (OnLeaderboardData);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			GameSparksLeaderboard.SavePlayerDistance (Random.Range(0, 10000));
			_isFetched = false;
		}
	}

	private void OnLeaderboardData(List<GameSparksLeaderboard.DistanceEntry> entries) {

		Debug.Log ("Leaderboard:");

		foreach (var entry in entries) {

			Debug.Log (string.Format("Entry: Username = {0}, Rank = {1}, Distance = {2}", entry.userName, entry.rank, entry.DISTANCE));
		}
	}
}
