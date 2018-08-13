using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using PlayFab;

public class BlueScreenScript : MonoBehaviour {

	public Text[] userNames, userScores;

	public Text playerHiScore, playerPosition, playerCurrentScore;

	public InputField playerUserName;

	private bool hiScore = true;

	// Use this for initialization
	void Start () {

		playerUserName.Select ();
		playerHiScore.text = GameManager.instance.score.ToString();
		playerCurrentScore.text = GameManager.instance.score.ToString();
		PostScore (Convert.ToInt32(playerHiScore.text));

		RefreshLeaderboard ();

	}
	
	// Update is called once per frame
	void Update () {
		if (hiScore){
			if (Input.GetKeyDown (KeyCode.F8)){
				PostUserName (playerUserName.text);
			}
		}
	}

	public void RefreshLeaderboard(){
		PlayFabClientAPI.GetLeaderboard( new PlayFab.ClientModels.GetLeaderboardRequest {
			// request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
			StartPosition = 0,
			StatisticName = "Score",
			MaxResultsCount = 9,

		},
			result => { 
				
				for (int i=0; i < result.Leaderboard.Count; i++) {
					userScores[i].text = result.Leaderboard[i].StatValue.ToString();
					userNames[i].text = result.Leaderboard[i].DisplayName;
				}
			},
			error => { Debug.LogError(error.GenerateErrorReport()); });
	}	

	public void GetPlayerPosition(int Score){
		PlayFabClientAPI.GetLeaderboardAroundPlayer( new PlayFab.ClientModels.GetLeaderboardAroundPlayerRequest {
			// request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
			MaxResultsCount = 1,
			StatisticName = "Score",
		},
			result => { Debug.Log("User statistics updated");
				playerPosition.text = (result.Leaderboard[0].Position+1).ToString() + ".";
				if (result.Leaderboard[0].StatValue > Score){
					playerHiScore.text = result.Leaderboard[0].StatValue.ToString();
					playerUserName.text = result.Leaderboard[0].DisplayName;
					playerUserName.interactable = false;
					hiScore = false;
				}
			},
			error => { Debug.LogError(error.GenerateErrorReport()); });
	}

	public void PostScore(int Score){

		PlayFabClientAPI.UpdatePlayerStatistics( new PlayFab.ClientModels.UpdatePlayerStatisticsRequest {
			// request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
			Statistics = new List<PlayFab.ClientModels.StatisticUpdate> {
				new PlayFab.ClientModels.StatisticUpdate { StatisticName = "Score", Value = Score },
			}
		},
			result => { Debug.Log("User statistics updated");
				GetPlayerPosition(Score);},
			error => { Debug.LogError(error.GenerateErrorReport()); });
	}

	public void PostUserName(string userName){

		PlayFabClientAPI.UpdateUserTitleDisplayName(new PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest() {
			DisplayName = userName
		}, 
			result => Debug.Log("Successfully updated user data"),
			error => {
				Debug.Log("Got error setting user data Ancestor to Arthur");
				Debug.Log(error.GenerateErrorReport());
			});

	}
}
