using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

  public Button Continue;
  public Button Exit;


  // Use this for initialization
	void Start () {
		Exit.onClick.AddListener(ExitGame);
		Continue.onClick.AddListener(ContinueGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void ExitGame()
  {
    GameManager.instance.AudioManager.PlaySFX("ButtonSelect"); 
    GameManager.instance.ExitGame();
  }

  void ContinueGame()
  {
    GameManager.instance.AudioManager.PlaySFX("ButtonSelect"); 
    GameManager.instance.ContinueGame();
  }
}
