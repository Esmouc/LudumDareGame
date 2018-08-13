using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

  public Button Play;
  public Button Exit;


  // Use this for initialization
	void Start () {
		Exit.onClick.AddListener(ExitGame);
		Play.onClick.AddListener(PlayGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void ExitGame()
  {
    GameManager.instance.AudioManager.PlaySFX("ButtonSelect"); 
    GameManager.instance.ExitGame();
  }

  void PlayGame()
  {
    GameManager.instance.AudioManager.PlaySFX("ButtonSelect"); 
    GameManager.instance.StartGame();
  }
}
