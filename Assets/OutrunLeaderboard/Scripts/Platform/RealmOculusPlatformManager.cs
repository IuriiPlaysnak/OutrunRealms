using UnityEngine;
//using Oculus.Avatar;
using Oculus.Platform;
using Oculus.Platform.Models;
using System.Collections;

public class RealmOculusPlatformManager : MonoBehaviour {

	void Awake () {
		Oculus.Platform.Core.Initialize();
	}

	private System.Action<string, string> _onOculusUserDataCallback;
	public void LoadData(System.Action<string, string> callback) {

		_onOculusUserDataCallback = callback;
		Oculus.Platform.Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
		Oculus.Platform.Request.RunCallbacks();
	}

	private void GetLoggedInUserCallback(Message<User> message) {
		if (message.IsError) {
			Debug.LogError (message.GetError().ToString());
		}
		else {
			if (_onOculusUserDataCallback != null)
				_onOculusUserDataCallback (message.Data.ID.ToString(), message.Data.OculusID);
		}
	}
}
