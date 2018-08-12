using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

  public Button Play;
  public Button Exit;

  private bool selected;

	// Use this for initialization
	void Start () {
    selected = false;
	}
	
	// Update is called once per frame
	void Update () {
    if(Input.GetJoystickNames().Length != 0) {
      if(Input.GetAxis("Horizontal") > 0.1f) {
        selected = true;
      } else if(Input.GetAxis("Horizontal") < -0.1f) {
        selected = false;
      }

      if(!selected)
        Play.Select();

      if(selected)
        Exit.Select();

      if(Input.GetKey("joystick button 0") || Input.GetKey("joystick button 1") || Input.GetKey("joystick button 2") || Input.GetKey("joystick button 3")) {
        GameManager.instance.mouse_visibility = false;
        if(!selected) GameManager.instance.StartGame();
        if(selected) GameManager.instance.ExitGame();
      }  
    }
	}
}
