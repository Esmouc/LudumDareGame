using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

	public Button Play;
	public Button Exit;
	public Button Tutorial;

	public GameObject TutorialObject;

	private bool selected;
	private bool tutorialSelected;

	// Use this for initialization
	void Start ()
	{
		selected = false;
		tutorialSelected = false;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetAxis ("Vertical") < -0.1f) {
			tutorialSelected = true;
		} 
			
		if (Input.GetJoystickNames ().Length != 0) {
			if (Input.GetAxis ("Horizontal") > 0.1f) {
				tutorialSelected = false;
				selected = true;
			} else if (Input.GetAxis ("Horizontal") < -0.1f) {
				tutorialSelected = false;
				selected = false;
			}
		}

		if (tutorialSelected){
			Tutorial.Select ();
		}else{
			if (!selected)
				Play.Select ();

			if (selected)
				Exit.Select ();
		}

		if (Input.GetKey ("joystick button 0") || Input.GetKey ("joystick button 2") || Input.GetKey ("joystick button 3")) {
			GameManager.instance.mouse_visibility = false;

			if (!tutorialSelected) {
				if (!selected)
					GameManager.instance.StartGame ();
				if (selected)
					GameManager.instance.ExitGame ();
			} else {
				TutorialObject.GetComponent<Animator> ().SetBool ("In", true);
			} 
		} 
	}

}
