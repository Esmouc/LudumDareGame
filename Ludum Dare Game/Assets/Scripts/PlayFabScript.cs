using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;

public class PlayFabScript : MonoBehaviour {

	private string current_playFabId;

	// Use this for initialization
	void Start () {

		CustomSignIn ();

	}

	void Update () {

		//if (Input.GetKeyDown (KeyCode.A))
		//	PlayerPrefs.DeleteAll ();

	}

	private void CustomSignIn() {

		PlayFabClientAPI.LoginWithCustomID(new PlayFab.ClientModels.LoginWithCustomIDRequest
			{
				CustomId = RetrieveID (),
				CreateAccount = true,
				TitleId = PlayFabSettings.TitleId
			}, (result) =>
			{

					Debug.Log("Signed In as " + result.PlayFabId);

					current_playFabId = result.PlayFabId;

			}, OnPlayFabError);

	}
		
	private void OnCloudResponse (PlayFab.ClientModels.ExecuteCloudScriptResult result) {
		//NI IDEA
	}

	private void OnPlayFabError(PlayFabError obj)
	{
		Debug.Log (obj.GenerateErrorReport());
	}

	public string RetrieveID() {

		string id = "";

		if (PlayerPrefs.GetString ("CustomID") == "") {

			for (int i = 0; i < UnityEngine.Random.Range (6, 9); i++) {
				id += UnityEngine.Random.Range (1000, 9999);
			}

			PlayerPrefs.SetString("CustomID", id);

		} else {

			id = PlayerPrefs.GetString ("CustomID");
		}

		return id;

	}

}